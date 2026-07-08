using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Variables")]
    [SerializeField] private float playerSpeed;
    private Vector2 inputDirection;
    private Vector3 moveDirection;

    public void GetInput(InputAction.CallbackContext obj)
    {
        inputDirection = obj.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        moveDirection = transform.right * inputDirection.x + transform.up * inputDirection.y;
        rb.AddForce(moveDirection.normalized * playerSpeed * 10f, ForceMode2D.Force);
    }
}
