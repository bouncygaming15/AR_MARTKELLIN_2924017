using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;

    private Rigidbody rb;
    private float horizontal;
    private float vertical;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizontal = 0f;
        vertical = 0f;

        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        if (kb.wKey.isPressed) vertical += 1f;   // maju
        if (kb.sKey.isPressed) vertical -= 1f;   // mundur
        if (kb.dKey.isPressed) horizontal += 1f; // kanan
        if (kb.aKey.isPressed) horizontal -= 1f; // kiri
    }

    void FixedUpdate()
    {
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 nextPosition = rb.position + direction * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(nextPosition);
    }
}