using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float horizontalInput = 0.0f;
    public float verticalInput = 0.0f;

    public Vector2 dirInput = Vector2.zero;

    public bool interact = false;
    public bool interact_extra = false;

    //Instanciar InputManager
    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.LogError("Warring, multiple input manager instances");
        }
    }

    private void Update()
    {
        // MOVEMENT INPUTS //
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        dirInput = new Vector2(horizontalInput, verticalInput).normalized;

        // INTERACT INPUT //
        if (interact)
        {
            interact = false;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            interact = true;
        }

        // INSTERACT EXTRA INPUT //
        if (interact_extra)
        {
            interact_extra = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            interact_extra = true;
        }
    }
}
