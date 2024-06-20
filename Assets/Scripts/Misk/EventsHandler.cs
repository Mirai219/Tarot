using System;

public static class EventsHandler
{
    private static Action FinishedSwapDelegate;
    public static event Action FinishedSwap
    {
        add
        {
            FinishedSwapDelegate += value;
        }
        remove
        {
            FinishedSwapDelegate -= value;
        }
    }

    public static void CallForFinishedSwap()
    {
        if(FinishedSwapDelegate != null)
        {
            FinishedSwapDelegate.Invoke();
        }
    }

    private static Action FinishedEliminateDelegate;

    public static event Action FinishedEliminate
    {
        add
        {
            FinishedEliminateDelegate += value;
        }
        remove
        {
            FinishedEliminateDelegate -= value;
        }
    }

    public static void CallForFinishedEliminate()
    {
        if (FinishedEliminateDelegate != null)
        {
            FinishedEliminateDelegate.Invoke();
        }
    }
}
