using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderData : ScriptableObject
{
    public GameManager.OrderTypes type;
    public bool completed = false;
}
