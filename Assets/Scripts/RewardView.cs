using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardView : MonoBehaviour
{
    [SerializeField] private string _lastTimeKey;
    [SerializeField] private string _activeSlotKey;

    #region Fields
    [Header("Time settings")]
    [SerializeField]
    public int TimeCooldown = 86400;
    [SerializeField]
    public int TimeDeadline = 172800;
    [Space]
    [Header("RewardSettings")]
    public List<Reward> Rewards;
    [Header("UI")]
    [SerializeField]
    public TMP_Text RewardTimer;
    [SerializeField]
    public Transform SlotsParent;
    [SerializeField]
    public Slider ProgressBar;
    [SerializeField]
    public SlotRewardView SlotPrefab;
    [SerializeField]
    public Button ResetButton;
    [SerializeField]
    public Button GetRewardButton;
    #endregion

    public int CurrentActiveSlot
    {
        get => PlayerPrefs.GetInt(_activeSlotKey);
        set => PlayerPrefs.SetInt(_activeSlotKey, value);
    }

    public DateTime? LastRewardTime
    {
        get
        {
            var data = PlayerPrefs.GetString(_lastTimeKey);
            if (string.IsNullOrEmpty(data))
                return null;
            return DateTime.Parse(data);
        }
        set
        {
            if (value != null)
                PlayerPrefs.SetString(_lastTimeKey, value.ToString());
            else
                PlayerPrefs.DeleteKey(_lastTimeKey);
        }
    }


    private void OnDestroy()
    {
        GetRewardButton.onClick.RemoveAllListeners();
        ResetButton.onClick.RemoveAllListeners();
    }

}
