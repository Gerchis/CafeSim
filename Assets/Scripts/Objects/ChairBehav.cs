using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ChairBehav : MonoBehaviour
{
    private Collider2D col;
    private GameManager.OrderTypes chairOrder = GameManager.OrderTypes.VOID;
    public ClientController client;

    private void Awake()
    {
        col = GetComponent<Collider2D>();

        if (!col.isTrigger)
        {
            col.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<ClientController>(out ClientController client))
        {
            client.ChangeState(ClientController.ClientStates.SIT);
            client.ChangeTarget(transform);
        }
    }

    public void AddOrder(GameManager.OrderTypes order)
    {
        chairOrder = order;
    }

    public void ClearOrder()
    {
        chairOrder = GameManager.OrderTypes.VOID;
    }

    public void ShowOrder()
    {
        if (chairOrder != GameManager.OrderTypes.VOID)
        {
            Debug.Log(chairOrder.ToString());
        }
    }

    public bool CheckOrder()
    {
        if (chairOrder != GameManager.OrderTypes.VOID)
        {
            if (InventoryManager.Instance.HasElement(chairOrder.ToString().ToLower()))
            {
                //Has the item in the inventory
                return true;
            }

            //Hasn't the item
            return false;
        }

        //If hasn't order, return false
        return false;
    }

    public void CompleteOrder()
    {
        client.ReciveOrder();
    }
}
