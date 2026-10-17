using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;

    private Rigidbody rb;
    private InputAction moveAction;
    private Vector2 input;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        moveAction = new InputAction("Move", InputActionType.Value);

        // Keyboard WASD (untuk tes di Editor)
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        // Joystick virtual / gamepad (untuk HP)
        moveAction.AddBinding("<Gamepad>/leftStick");
    }

    void OnEnable()  { moveAction.Enable(); }
    void OnDisable() { moveAction.Disable(); }

    void Update()
    {
        input = Vector2.ClampMagnitude(moveAction.ReadValue<Vector2>(), 1f);
    }

    void FixedUpdate()
    {
        Vector3 direction = new Vector3(input.x, 0f, input.y);
        Vector3 nextPosition = rb.position + direction * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(nextPosition);
    }
}