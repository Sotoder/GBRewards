using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField]
    private List<RewardView> _rewardViews;

    [SerializeField]
    private RewardsContainerSwitcher _rewardsContainerSwitcher;

    private RewardController _controller;
    private MementoSaver _mementoSaver;
    private SaveLoadDataController _saveDataController;

    private List<IDisposable> _disposables = new List<IDisposable>();

    void Start()
    {
        _mementoSaver = new MementoSaver(new List<ISavebleRewardView>(_rewardViews));
        var loadManager = new LoadManager(new List<ILoadableRewardView>(_rewardViews));
        _saveDataController = new SaveLoadDataController(_mementoSaver, new List<IViewWithSaveAndLoadButton>(_rewardViews), loadManager);

        _controller = new RewardController(_rewardViews);

        _rewardsContainerSwitcher.Init(new List<ISwitchableRewardView>(_rewardViews), _controller); 

        _disposables.Add(_mementoSaver);
        _disposables.Add(_controller);
        _disposables.Add(_saveDataController);
    }

    private void OnDestroy()
    {
        foreach (var element in _disposables)
        {
            element.Dispose();
        }
    }
}
