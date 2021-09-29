using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractGetItem : MonoBehaviour, IInteractable
{
    private PlayerController player;

    [SerializeField] private string[] required;
    [SerializeField] private string[] recived;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player == null && collision.gameObject.TryGetComponent<PlayerController>(out PlayerController _player))
        {
            player = _player;
        }

        if (player != null && player.gameObject == collision.gameObject)
        {
            player.SetObjectToInteract(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (player == collision)
        {
            player.ClearObjectToInteract(this);
        }
    }

    private bool CheckRequirements()
    {
        //Has no requirements
        if (required.Length <= 0)
        {
            return true;
        }

        //Requirement not in the inventory
        foreach (string requirement in required)
        {
            if (!InventoryManager.Instance.HasElement(requirement))
            {
                return false;
            }
        }

        //Reuirement in the inventory
        return true;
    }

    public void Interact()
    {
        if (CheckRequirements())
        {
            foreach (string item in recived)
            {
                InventoryManager.Instance.AddElement(item);
                Debug.Log(item);
            }
        }
    }

    public void InteractExtra()
    {

    }
}
