using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Action Settings")]
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpForce;

    [Header("Camera Reference")]
    [SerializeField] GameObject cam;

    IA_PlayerActions myActions;
    Rigidbody rb;
    Animator animator;
    bool grounded;

    Vector2 moveInput;
    Vector3 moveDirection;
    private void OnEnable()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
        if (myActions == null)
            myActions = new IA_PlayerActions();
        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        myActions.Enable();

        myActions.Player.Jump.performed += OnJump;
    }
    void Update()
    {
        moveInput = myActions.Player.Move.ReadValue<Vector2>();

        Vector3 camForward = cam.transform.forward;
        Vector3 camRight = cam.transform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward = camForward.normalized;
        camRight = camRight.normalized;

        moveDirection = (camForward * moveInput.y) + (camRight * moveInput.x);

        grounded = Physics.Raycast(transform.position, -transform.up, transform.localScale.y / 2 + 0.5f);
        animator.SetBool("IsGrounded", grounded);

        if (moveDirection != Vector3.zero)
        {
            animator.transform.forward = moveDirection;
            animator.SetBool("IsRunning", true);
        }
        if (moveDirection == Vector3.zero) 
        {
            animator.SetBool("IsRunning", false);
        }
    }
    private void FixedUpdate()
    {
        
        rb.AddForce(moveDirection * movementSpeed, ForceMode.Acceleration);
    }
    void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && grounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
