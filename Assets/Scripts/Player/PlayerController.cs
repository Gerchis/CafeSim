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

    private Animator anim;
    private enum DIR
    {
        UP,
        DOWN,
        RIGHT,
        LEFT,
        NONE
    }

    private DIR dir = DIR.NONE;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        AnimationManagement();

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

    void AnimationManagement()
    {
        if (InputManager.Instance.dirInput != Vector2.zero)
        {
            anim.SetBool("Walk", true);

            anim.SetBool("DirUp", false);
            anim.SetBool("DirDown", false);
            anim.SetBool("DirRight", false);
            anim.SetBool("DirLeft", false);

            if (Mathf.Abs(rb.velocity.y) > Mathf.Abs(rb.velocity.x) && rb.velocity.y > 0)
            {
                anim.SetBool("DirUp",true);
                dir = DIR.UP;
            }
            else if (Mathf.Abs(rb.velocity.y) > Mathf.Abs(rb.velocity.x) && rb.velocity.y < 0)
            {
                anim.SetBool("DirDown",true);
                dir = DIR.DOWN;
            }
            else if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.y) && rb.velocity.x > 0)
            {
                anim.SetBool("DirRight", true);
                dir = DIR.RIGHT;
            }
            else if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.y) && rb.velocity.x < 0)
            {
                anim.SetBool("DirLeft", true);
                dir = DIR.LEFT;
            }
        }
        else
        {
            anim.SetBool("Walk", false);

            if (dir != DIR.NONE)
            {
                dir = DIR.NONE;
            }
        }
    }
}
