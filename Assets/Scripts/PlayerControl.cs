using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    private float rotationInput;

    void Update()
    {
        HandleInput();
        RotateHelix();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButton(0))
        {
            rotationInput = Input.GetAxis("Mouse X");
        }
        else
        {
            rotationInput = 0;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rotationInput = -1;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rotationInput = 1;
        }

    }

    private void RotateHelix()
    {
        transform.Rotate(0, rotationInput * rotationSpeed * Time.deltaTime, 0);
    }
}
