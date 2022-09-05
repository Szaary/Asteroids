using System;

public class EventBroker
{
    public static event Action ShotAction;
    public static void CallShotAction()
    {
        ShotAction?.Invoke();
    }

    public static event Action ExplodeAction;
    public static void CallExplodeAction()
    {
        ExplodeAction?.Invoke();
    }

}
