using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardController: ISwitchableRewardController, IDisposable
{
    private readonly List<RewardView> _rewardViews = new List<RewardView>();
    private RewardView _curentRewardView;

    public RewardController(List<RewardView> rewardViews)
    {
        _rewardViews = rewardViews;
        foreach (var view in _rewardViews)
        {
            InitSlots(view);
            SubscribeButtons(view);
        }
        _curentRewardView = _rewardViews[0];
        _curentRewardView.StartCoroutine(UpdateCoroutine());
    }

    void ISwitchableRewardController.SetCurentRewardView(int rewardViewIndex)
    {
        for (int i = 0; i < _rewardViews.Count; i++)
        {
            if (i == rewardViewIndex)
            {
                _curentRewardView = _rewardViews[i];
                RefreshRewardState();
                RefreshUi();
            }
        }
    }

    private IEnumerator UpdateCoroutine()
    {
        while (true)
        {
            Update();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void Update()
    {
        RefreshRewardState();
        RefreshUi();
    }

    private void RefreshRewardState()
    {
        _curentRewardView.RewardReceived = false;
        if (_curentRewardView.LastRewardTime.HasValue)
        {
            var timeSpan = DateTime.UtcNow - _curentRewardView.LastRewardTime.Value;
            if (timeSpan.TotalSeconds > _curentRewardView.TimeDeadline)
            {
                _curentRewardView.LastRewardTime = null;
                _curentRewardView.CurrentActiveSlot = 0;
            }
            else if(timeSpan.TotalSeconds < _curentRewardView.TimeCooldown)
            {
                _curentRewardView.RewardReceived = true;
            }
        }
    }

    private void RefreshUi()
    {
        _curentRewardView.GetRewardButton.interactable = !_curentRewardView.RewardReceived;

        for (var i = 0; i < _curentRewardView.Rewards.Count; i++)
        {
            _curentRewardView.Slots[i].SetData(_curentRewardView.Rewards[i], i+1, i <= _curentRewardView.CurrentActiveSlot);
        }

        DateTime nextDailyBonusTime =
            !_curentRewardView.LastRewardTime.HasValue
                ? DateTime.MinValue
                : _curentRewardView.LastRewardTime.Value.AddSeconds(_curentRewardView.TimeCooldown);
        var delta = nextDailyBonusTime - DateTime.UtcNow;
        if (delta.TotalSeconds < 0)
            delta = new TimeSpan(0);

        _curentRewardView.ProgressBar.value = (_curentRewardView.TimeCooldown - (float)delta.TotalSeconds) / _curentRewardView.TimeCooldown;

        _curentRewardView.RewardTimer.text = delta.ToString(@"dd\.hh\:mm\:ss");
    }

    private void InitSlots(RewardView rewardView)
    {
        rewardView.Slots = new List<SlotRewardView>();
        for (int i = 0; i < rewardView.Rewards.Count; i++)
        {
            var reward = rewardView.Rewards[i];
            var slotInstance = GameObject.Instantiate(rewardView.SlotPrefab, rewardView.SlotsParent, false);
            slotInstance.SetData(reward, i+1, false);
            rewardView.Slots.Add(slotInstance);
        }
    }

    private void SubscribeButtons(RewardView rewardView)
    {
        rewardView.GetRewardButton.onClick.AddListener(ClaimReward);
        rewardView.ResetButton.onClick.AddListener(ResetReward);
    }

    private void ResetReward()
    {
        _curentRewardView.LastRewardTime = null;
        _curentRewardView.CurrentActiveSlot = 0;
    }

    private void ClaimReward()
    {
        if (_curentRewardView.RewardReceived)
            return;
        var reward = _curentRewardView.Rewards[_curentRewardView.CurrentActiveSlot];
        switch (reward.Type)
        {
            case RewardType.None:
                break;
            case RewardType.Wood:
                CurrencyWindow.Instance.AddWood(reward.Count);
                break;
            case RewardType.Diamond:
                CurrencyWindow.Instance.AddDiamond(reward.Count);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        _curentRewardView.LastRewardTime = DateTime.UtcNow;
        _curentRewardView.CurrentActiveSlot = (_curentRewardView.CurrentActiveSlot + 1) % _curentRewardView.Rewards.Count;
        _curentRewardView.UserGetReward?.Invoke();
        RefreshRewardState();
    }

    public void Dispose()
    {
        foreach(var view in _rewardViews)
        {
            view.GetRewardButton.onClick.RemoveListener(ClaimReward);
            view.ResetButton.onClick.RemoveListener(ResetReward);
        }
    }
}
