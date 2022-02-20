using System;

[Serializable]
public class CurrencyViewMemento
{
    public int Wood;
    public int Diamond;

    public CurrencyViewMemento (int woodCont, int diamondCount)
    {
        Wood = woodCont;
        Diamond = diamondCount;
    }
}