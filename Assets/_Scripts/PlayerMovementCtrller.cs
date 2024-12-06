using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementCtrller : MonoBehaviour
{
    public float moveSpeed = 0f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Vector2 movementInput;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // if movement input is not 0, moveeeee
        if (movementInput != Vector2.zero)
        {
            // move
            int count = rb.Cast(
                movementInput, // x-y (1; -1) 
                movementFilter, // cho phép va chạm sảy ra ở đâu, giữa ai với ai XD
                castCollisions, // danh sách collision
                moveSpeed * Time.fixedDeltaTime * collisionOffset
            );
        }
    }

    void ONMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
