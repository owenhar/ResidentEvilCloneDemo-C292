using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyEvents
{
    public static UnityEvent<bool> shotFired = new UnityEvent<bool>();
    public static UnityEvent zombieKilled = new UnityEvent();
}
