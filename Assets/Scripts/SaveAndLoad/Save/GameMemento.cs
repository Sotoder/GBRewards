using System;
using System.Collections.Generic;

[Serializable]
public class GameMemento
{
    public List<RewardViewMemento> RewardViewMementos;
    public CurrencyViewMemento CurrencyMemento;

    public GameMemento (List<RewardViewMemento> rewardViewMementos, CurrencyViewMemento currencyViewMemento)
    {
        RewardViewMementos = new List<RewardViewMemento>(rewardViewMementos);
        CurrencyMemento = currencyViewMemento;
    }
}