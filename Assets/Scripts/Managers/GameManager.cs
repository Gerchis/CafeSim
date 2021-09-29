using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum OrderTypes
    {
        VOID,
        CAFE,
        TE,
        CROISANT,
        COUNT
    }

    // Instance the game manager //
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.LogError("Multiple instances of game managers");
        }
    }
}
