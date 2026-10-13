using UnityEngine;
using UnityEngine.InputSystem; // Input System baru

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement3D : MonoBehaviour
{
    [Header("Kamera")]
    public Camera playerCamera;
    public float mouseSensitivity = 0.1f;
    public float lookXLimit = 90f;

    [Header("Gerakan")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float crouchSpeed = 3f;
    public float jumpPower = 7f;
    public float gravity = 20f;

    [Header("Crouch")]
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0f;
    private CharacterController characterController;
    private bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Keyboard kb = Keyboard.current;
        Mouse mouse = Mouse.current;
        if (kb == null || mouse == null) return;

        // ----- Input arah (W A S D) -----
        float inputVertical = 0f;
        float inputHorizontal = 0f;

        if (canMove)
        {
            if (kb.wKey.isPressed) inputVertical += 1f;   // maju
            if (kb.sKey.isPressed) inputVertical -= 1f;   // mundur
            if (kb.dKey.isPressed) inputHorizontal += 1f; // kanan
            if (kb.aKey.isPressed) inputHorizontal -= 1f; // kiri
        }

        // ----- Crouch & kecepatan -----
        bool isCrouching = canMove && kb.rKey.isPressed;
        bool isRunning = kb.leftShiftKey.isPressed && !isCrouching;

        float currentSpeed = isCrouching ? crouchSpeed : (isRunning ? runSpeed : walkSpeed);
        characterController.height = isCrouching ? crouchHeight : defaultHeight;

        // ----- Gerak horizontal -----
        Vector3 horizontalMove = transform.forward * inputVertical + transform.right * inputHorizontal;
        if (horizontalMove.magnitude > 1f) horizontalMove.Normalize();
        horizontalMove *= currentSpeed;

        float verticalVelocity = moveDirection.y;
        moveDirection = horizontalMove;

        // ----- Lompat (Spasi) -----
        if (characterController.isGrounded)
        {
            if (verticalVelocity < 0f) verticalVelocity = -2f;

            if (kb.spaceKey.isPressed && canMove)
            {
                verticalVelocity = jumpPower;
            }
        }

        // ----- Gravitasi -----
        verticalVelocity -= gravity * Time.deltaTime;
        moveDirection.y = verticalVelocity;

        characterController.Move(moveDirection * Time.deltaTime);

        // ----- Kamera (Mouse) -----
        if (canMove)
        {
            Vector2 mouseDelta = mouse.delta.ReadValue();

            rotationX += -mouseDelta.y * mouseSensitivity;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

            transform.rotation *= Quaternion.Euler(0f, mouseDelta.x * mouseSensitivity, 0f);
        }
    }
}