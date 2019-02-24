using Slantar.Services;
using UnityEngine;

public class EventsTest : MonoBehaviour
{
    public void Start()
    {
        Core.Events.Subscribe<ClassA>(gameObject, PrintA);
        Core.Events.Subscribe<ClassB>(gameObject, PrintB);
        Core.Events.Subscribe<ClassC>(gameObject, PrintC);

        Core.Events.Chain<ClassC, ClassA>(gameObject, PrintAC);

        Core.Events.Trigger(new ClassA());
        Core.Events.Trigger(new ClassB());
        Core.Events.Trigger(new ClassC());

        Debug.Log("Unsubscribing ClassA event");
        Core.Events.Unsubscribe<ClassA>(gameObject, PrintA);
        Core.Events.Trigger(new ClassA());
        Core.Events.Trigger(new ClassC());
    }

    private ClassA PrintAC(ClassC arg1)
    {
        Debug.Log("If trigger C, triggers A");
        return new ClassA();
    }

    private void PrintA(ClassA e)
    {
        Debug.Log("Event A");
    }

    private void PrintB(ClassB e)
    {
        Debug.Log("Event B");
    }

    private void PrintC(ClassC e)
    {
        Debug.Log("Event C");
    }
}


public class ClassA { }

public class ClassB { }

public class ClassC { }