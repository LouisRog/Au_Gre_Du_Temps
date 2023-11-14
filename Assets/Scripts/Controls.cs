using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Controls : MonoBehaviour
{
    float HorizontalMove;
    [SerializeField] public float speed = 50;
    [SerializeField] public float mouseSpeed = 500;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void Update()
    {
        HandleKeyboardInput();
        HandleMouseInput();
    }

    private void HandleKeyboardInput()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        transform.Rotate(0, -horizontalMove * speed * Time.deltaTime, 0);
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButton(0)) // Check if the left mouse button is held down
        {
            float mouseX = Input.GetAxis("Mouse X");

            // Rotate the camera based on mouse movement
            transform.Rotate(Vector3.up, mouseX * mouseSpeed * Time.deltaTime);

        }
    }
}
