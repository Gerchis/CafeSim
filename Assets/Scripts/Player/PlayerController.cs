using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Variables")]
    public float speed = 0.0f;
    public float accel = 0.0f;
    [Space]

    private Rigidbody2D rb;
    private Vector2 targetVelocity = Vector2.zero;
    private Vector2 actualVelocity = Vector2.zero;

    private IInteractable objectToInteract;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (InputManager.Instance.interact && objectToInteract != null)
        {
            objectToInteract.Interact();
        }

        if (InputManager.Instance.interact_extra && objectToInteract != null)
        {
            objectToInteract.InteractExtra();
        }
    }

    private void FixedUpdate()
    {

        targetVelocity = InputManager.Instance.dirInput * speed;

        actualVelocity = Vector2.Lerp(actualVelocity, targetVelocity, accel * Time.fixedDeltaTime);

        rb.velocity = actualVelocity;
    }

    public void SetObjectToInteract(IInteractable interactable)
    {
        objectToInteract = interactable;
    }

    public void ClearObjectToInteract(IInteractable interactable)
    {
        if (objectToInteract == interactable)
        {
            objectToInteract = null;
        }
    }
}
