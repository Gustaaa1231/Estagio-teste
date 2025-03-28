using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private Vector3 inputs;

    public float speed = 5f;
    public float runSpeed = 30f;
    public float jumpForce = 6f;
    private bool isGrounded;

    public Transform cameraTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!enabled) return;
        inputs.Set(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 desiredMoveDirection = forward * inputs.z + right * inputs.x;

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : speed;

        Vector3 movement = desiredMoveDirection * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);

        if (inputs != Vector3.zero)
        {
            animator.SetBool("walk", !isRunning);
            animator.SetBool("run", isRunning);
            transform.forward = Vector3.Slerp(transform.forward, desiredMoveDirection, Time.deltaTime * 10);
        }
        else
        {
            animator.SetBool("walk", false);
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetBool("jump", true);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("jump", false);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
