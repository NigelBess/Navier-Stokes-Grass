using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpSpeed = 4;
    [SerializeField] private Transform camera;
    private CharacterController characterController;
    private Vector3 velocity;
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        if (characterController.isGrounded)
        {
            Debug.Log("grounded");
            // We are grounded, so recalculate
            // move direction directly from axes
            Vector2 dir = new Vector2(Input.GetAxis("Forward"), -Input.GetAxis("Right"));
            Vector3 camDir = camera.forward;
            camDir.y = 0;
            velocity = (camDir.normalized * dir.x + Vector3.Cross(camera.forward,Vector3.up).normalized*dir.y).normalized;
            velocity = velocity * speed;
            if (Input.GetButton("Jump"))
            {
                velocity.y = jumpSpeed;
            }
        }
        else
        {
            velocity.y += Physics.gravity.y * Time.deltaTime;
        }
        characterController.Move(velocity * Time.deltaTime);
    }
}
