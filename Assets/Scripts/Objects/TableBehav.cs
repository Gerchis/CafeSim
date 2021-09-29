using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TableBehav : MonoBehaviour,IInteractable
{
    private PlayerController player;
    [SerializeField] ChairBehav[] chairs;

    private void Awake()
    {
        Collider2D col = GetComponent<Collider2D>();

        if (!col.isTrigger)
        {
            col.isTrigger = true;
        }
    }

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

    public void Interact()
    {
        foreach (ChairBehav chair in chairs)
        {        
            if (chair.CheckOrder())
            {
                chair.CompleteOrder();
            }
        }
    }

    public void InteractExtra()
    {        
        foreach (ChairBehav chair in chairs)
        {
            chair.ShowOrder();
        }
    }
}
