using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardsContainerSwitcher : MonoBehaviour
{
    [SerializeField] private Transform _horizontalLayout;
    [SerializeField] private ButtonView _buttonViewPrefab;

    private ISwitchableRewardController _rewardController;
    private List<ButtonView> _buttonViews = new List<ButtonView>();
    private List<GameObject> _uiContainers = new List<GameObject>();

    public void Init(List<RewardView> rewardViews, ISwitchableRewardController rewardController)
    {
        _rewardController = rewardController;

        for (int i = 0; i < rewardViews.Count; i++)
        {
            var button = Instantiate<ButtonView>(_buttonViewPrefab, _horizontalLayout);
            int index = i;
            button.ViewButton.onClick.AddListener(() => SwitchContainer(index, button, _uiContainers[index]));
            button.ButtonText.text = rewardViews[i].Name;
            _buttonViews.Add(button);
            _uiContainers.Add(rewardViews[i].UIContainer);

            SwitchContainer(0, _buttonViews[0], _uiContainers[0]);
        }
    }

    private void SwitchContainer(int index, ButtonView button, GameObject uiContainer)
    {
        foreach (var buttonView in _buttonViews)
        {
            if(buttonView == button) buttonView.ButtonImage.color = Color.green;
            else buttonView.ButtonImage.color = Color.white;
        }
        foreach (var container in _uiContainers)
        {
            if (container == uiContainer) container.SetActive(true);
            else container.SetActive(false);
        }

        _rewardController.SetCurentRewardView(index);
    }

    public void OnDestroy()
    {
        foreach (var button in _buttonViews)
        {
            button.ViewButton.onClick.RemoveAllListeners();
        }
    }
}
