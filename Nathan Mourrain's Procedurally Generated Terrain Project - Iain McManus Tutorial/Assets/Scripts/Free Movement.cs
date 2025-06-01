using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSpeedX = 2f;
    public float lookSpeedY = 2f;
    private float rotationX = 0f;
    private float rotationY = 0f;

    public Camera playerCamera; // Drag the Camera into this slot in the Inspector

    void Start()
    {
        if (playerCamera == null)
            playerCamera = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Unlock while holding Left Shift
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        HandleMovement();
        HandleLook();
    }

    void HandleMovement()
    {
        Vector3 direction = Vector3.zero;

        // Use camera's orientation for movement directions
        if (Input.GetKey(KeyCode.W)) direction += playerCamera.transform.forward;
        if (Input.GetKey(KeyCode.S)) direction -= playerCamera.transform.forward;
        if (Input.GetKey(KeyCode.D)) direction += playerCamera.transform.right;
        if (Input.GetKey(KeyCode.A)) direction -= playerCamera.transform.right;
        if (Input.GetKey(KeyCode.E)) direction += playerCamera.transform.up;
        if (Input.GetKey(KeyCode.Q)) direction -= playerCamera.transform.up;

        direction = direction.normalized;

        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void HandleLook()
    {
        // Handle mouse look (dragging the mouse)
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the camera horizontally (around Y axis)
        transform.Rotate(Vector3.up * mouseX * lookSpeedX);

        // Rotate the camera vertically (around X axis)
        rotationX -= mouseY * lookSpeedY;
        //rotationX = Mathf.Clamp(rotationX, -80f, 80f); // Prevent camera from rotating too far up or down

        rotationY += mouseX * lookSpeedX;
        //rotationY = Mathf.Clamp(rotationY, -80f, 80f);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }
}
