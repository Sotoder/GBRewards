using System.Collections.Generic;

public class LoadManager
{
    private List<ILoadableRewardView> _loadebleRewardViews = new List<ILoadableRewardView>();
    public LoadManager(List<ILoadableRewardView> loadableViews)
    {
        _loadebleRewardViews = loadableViews;
    }

    public void Load(GameMemento gameMemento)
    {
        foreach (var loadebleView in _loadebleRewardViews)
        {
            foreach (var memento in gameMemento.RewardViewMementos)
            {
                if (loadebleView.ID == memento.ViewID)
                {
                    loadebleView.Load(memento);
                    break;
                }
            }
        }

        CurrencyWindow.Instance.Load(gameMemento.CurrencyMemento);
    }
}