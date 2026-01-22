using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components to add manually")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 moveInput;

    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
                Debug.LogWarning($"Rigidbody2D no asignado y no encontrado en '{gameObject.name}'. Asigna uno en el inspector.");
        }

        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
                animator = GetComponentInChildren<Animator>();

            if (animator == null)
                Debug.LogWarning($"Animator no asignado y no encontrado en '{gameObject.name}'. Asigna uno en el inspector.");
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        animator.SetBool("isWalking", true);

        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("lastInputX", moveInput.x);
            animator.SetFloat("lastInputY", moveInput.y);
        }

        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("inputX", moveInput.x);
        animator.SetFloat("inputY", moveInput.y);
    }
}
