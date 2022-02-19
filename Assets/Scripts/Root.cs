using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField]
    private RewardView _dalyRewardView;

    [SerializeField]
    private RewardView _weeklyRewardView;

    [SerializeField]
    private RewardsContainerSwitcher _rewardsContainerSwitcher;

    private RewardController _dalyController;
    private RewardController _weeklyController;

    void Start()
    {
        _dalyController = new RewardController(_dalyRewardView);
        _weeklyController = new RewardController(_weeklyRewardView);

        _rewardsContainerSwitcher.Init();
    }
}
