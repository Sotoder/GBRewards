using System;
using System.Collections.Generic;

public class MementoSaver: IDisposable
{
    private List<ISavebleRewardView> _savebleViews = new List<ISavebleRewardView>();
    private List<GameMemento> _gameMementos = new List<GameMemento>(8);

    public MementoSaver(List<ISavebleRewardView> rewardViews)
    {
        _savebleViews = rewardViews;

        foreach (var view in _savebleViews)
        {
            view.UserGetReward += SaveMemento;
        }
    }

    private void SaveMemento()
    {
        List<RewardViewMemento> viewMementos = new List<RewardViewMemento>();

        foreach (var view in _savebleViews)
        {
            viewMementos.Add(
                new RewardViewMemento (
                    view.ID,
                    view.CurrentActiveSlot,
                    view.LastRewardTime.ToString())
                );
        }

        CurrencyViewMemento currency = new CurrencyViewMemento(CurrencyWindow.Instance.GetWoodCount(), CurrencyWindow.Instance.GetDiamondCount()); 

        if (_gameMementos.Count > 7)
        {
            _gameMementos.RemoveAt(0);
        }

        _gameMementos.Add(new GameMemento(viewMementos, currency));
    }

    public GameMemento GetLastMementoForSave()
    {
        if (_gameMementos.Count == 0) return null;

        return _gameMementos[_gameMementos.Count - 1];
    }

    public void Dispose()
    {
        foreach (var view in _savebleViews)
        {
            view.UserGetReward -= SaveMemento;
        }
    }
}