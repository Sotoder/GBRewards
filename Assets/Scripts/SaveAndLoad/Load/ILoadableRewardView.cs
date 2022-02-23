public interface ILoadableRewardView
{
    int ID { get; }
    void Load(RewardViewMemento revardViewMemento);
}