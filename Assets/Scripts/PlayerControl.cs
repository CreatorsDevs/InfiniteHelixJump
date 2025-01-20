using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    private float rotationInput;

    void Update()
    {
        if (GameManager.Instance.GameStarted)
        {
            HandleInput();
        }
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
        RotateHelix();

    }

    private void RotateHelix()
    {
        transform.Rotate(0, rotationInput * rotationSpeed * Time.deltaTime, 0);
    }
}
