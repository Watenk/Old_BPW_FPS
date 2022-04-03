using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterControlScript : MonoBehaviour
{
    private UI UIScript;

    public Transform Player;
    public Transform Camera;
    public CharacterController controller;
    public float Speed = 13f;
    public float gravity = 9.8f;
    public float MouseSensitivity = 2f;
    private float xRotation = 0f;
    //Jump
    public bool IsOnGround;
    public float JumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float JumpCooldown = 3f;
    public Vector3 Velocity;

    //HP
    public float PlayerHealth = 100f;


    private void Start()
    {
        //Lock Cursor
        Cursor.lockState = CursorLockMode.Locked;

        UIScript = FindObjectOfType<UI>();
    }

    void Update()
    {
        if (PlayerHealth >= 0.01)
        {
            MouseMovement();
            KeyboardMovement();
            Jump();
        }
        GroundCheck();
        Gravity();
        Lose();
    }

    //Functions:

    void MouseMovement()
    {
        //Get MouseX and Y
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity;

        //Calc xRotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Left and Right
        Player.Rotate(Vector3.up * mouseX);

        //Up and Down
        Camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void KeyboardMovement()
    {
        float keyboardX = Input.GetAxis("Horizontal");
        float keyboardZ = Input.GetAxis("Vertical");

        Vector3 location = transform.right * keyboardX + transform.forward * keyboardZ;
        controller.Move(location * Speed * Time.deltaTime);
    }

    void GroundCheck()
    {
        IsOnGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (IsOnGround && Velocity.y < 0)
        {
            Velocity.y = -2f;
        }
    }

    void Gravity()
    {
        Velocity.y -= gravity * Time.deltaTime;
        controller.Move(Velocity * Time.deltaTime);
    }


    void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsOnGround == true)
        {
            Velocity.y = JumpHeight;
        }
    }

    void Lose()
    {
        if (PlayerHealth <= 0f)
        {
            UIScript.YouLost.gameObject.SetActive(true);
            UIScript.pressEtoStartAgain.gameObject.SetActive(true);

            if (Input.GetKeyDown("e"))
            {
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}

