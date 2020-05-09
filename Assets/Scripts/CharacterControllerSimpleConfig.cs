using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerSimpleConfig : MonoBehaviour
{
    public float speed;
    public float impulseJump;
    public float gravity;
    
    Vector3 moveDirection;
    Vector3 verticalDirection;
    CharacterController controller;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        moveDirection = transform.right * x + transform.forward * z;

        controller.Move(moveDirection * speed * Time.deltaTime);

        if (controller.isGrounded)
        {
            verticalDirection.y = -2f;
            if (Input.GetButtonDown("Jump"))
            {
                verticalDirection.y = Mathf.Sqrt(impulseJump * -2 * gravity);
            }
        }
        verticalDirection.y += gravity * Time.deltaTime;

        controller.Move(verticalDirection * Time.deltaTime);
    }
}
