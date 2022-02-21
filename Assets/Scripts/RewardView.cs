using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardView : MonoBehaviour, ISavebleRewardView, IViewWithSaveAndLoadButton, ILoadableRewardView, ISwitchableRewardView
{
    #region InspectorFields
    [SerializeField]
    private string _name;
    [Space]
    [Header("PlayerPref Settings")]
    [SerializeField] 
    private string _lastTimeKey;
    [SerializeField] 
    private string _activeSlotKey;
    [Space]
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
    private GameObject _uiContainer;
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
    private Button _saveButton;
    [SerializeField]
    private Button _loadButton;
    [SerializeField]
    public Button GetRewardButton;
    #endregion

    public List<SlotRewardView> Slots;
    public bool RewardReceived = false;

    private int _id;

    public int ID => _id;
    public Action UserGetReward { get; set; }
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
            {
                PlayerPrefs.SetString(_lastTimeKey, value.ToString());
            }
            else
                PlayerPrefs.DeleteKey(_lastTimeKey);
        }
    }

    public Button SaveButton { get => _saveButton; }
    public Button LoadButton { get => _loadButton; }
    public string Name { get => _name; }
    public GameObject UIContainer { get => _uiContainer; }

    private void Awake()
    {
        _id = Animator.StringToHash(Name);
    }
    public void Load(RewardViewMemento rewardViewMemento)
    {
        CurrentActiveSlot = rewardViewMemento.CurrentActiveSlot;
        if (rewardViewMemento.LastRewardTime == "") LastRewardTime = null;
        else LastRewardTime =  DateTime.Parse(rewardViewMemento.LastRewardTime);
    }

    private void OnDestroy()
    {
        GetRewardButton.onClick.RemoveAllListeners();
        ResetButton.onClick.RemoveAllListeners();
        SaveButton.onClick.RemoveAllListeners();
        LoadButton.onClick.RemoveAllListeners();
    }
}
