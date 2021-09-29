using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    Dictionary<string, int> inventory = new Dictionary<string, int>();

    public static InventoryManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.LogError("Multiple instances of inventory managers");
        }
    }

    public void AddElement(string element)
    {
        if (inventory.ContainsKey(element))
        {
            inventory[element] = inventory[element] + 1;

            return;
        }

        inventory.Add(element, 1);
    }

    public void RemoveElement(string element)
    {
        if (inventory.ContainsKey(element))
        {
            inventory[element] = inventory[element] - 1;

            if (inventory[element] <= 0)
            {
                inventory.Remove(element);
            }
        }
    }

    public bool HasElement(string element)
    {
        if (inventory.ContainsKey(element))
        {
            return true;
        }
        return false;
    }
}
