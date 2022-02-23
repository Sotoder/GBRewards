using System;
using UnityEngine.UI;

public interface ISavebleRewardView
{
    int ID { get; }
    int CurrentActiveSlot { get; set; }
    DateTime? LastRewardTime { get; set; }
    Action UserGetReward { get; set; }
    Action UserResetView { get; set; }
}