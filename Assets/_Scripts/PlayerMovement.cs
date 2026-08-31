using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Action Settings")]
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpForce;


    IA_PlayerActions myActions;
    Rigidbody rb;

    Vector2 moveInput;
    private void OnEnable()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
        if (myActions == null)
            myActions = new IA_PlayerActions();

        myActions.Enable();

        myActions.Player.Jump.performed += OnJump;
    }
    void Start()
    {
        
    }
    void Update()
    {
        moveInput = myActions.Player.Move.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(moveInput.x, 0, moveInput.y) * movementSpeed, ForceMode.Acceleration);
    }
    void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
