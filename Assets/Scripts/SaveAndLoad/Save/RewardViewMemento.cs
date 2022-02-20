using System;

[Serializable]
public class RewardViewMemento
{
    public int ViewID;
    public int CurrentActiveSlot;
    public string LastRewardTime;

    public RewardViewMemento (int id, int activeSlot, string time)
    {
        ViewID = id;
        CurrentActiveSlot = activeSlot;
        LastRewardTime = time;
    }
}