using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform cameraStand;

    public float mouseSpeed = 3.0f;
    public float movementSpeed = 3.0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey("w"))
            cameraStand.position += transform.forward * movementSpeed;
        if (Input.GetKey("s"))
            cameraStand.position += transform.forward * -movementSpeed;
        if (Input.GetKey("d"))
            cameraStand.position += cameraStand.right * movementSpeed;
        if (Input.GetKey("a"))
            cameraStand.position += cameraStand.right * -movementSpeed;
        if (Input.GetKey("space"))
            cameraStand.position += Vector3.up * movementSpeed;
        if (Input.GetKey("left shift"))
            cameraStand.position += Vector3.down * movementSpeed;
    }

    private void Update()
    {
        if (!Input.GetKey("e"))
        {
            cameraStand.Rotate(0, Input.GetAxis("Mouse X") * mouseSpeed, 0);
            transform.Rotate(-Input.GetAxis("Mouse Y") * mouseSpeed, 0, 0);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else Cursor.lockState = CursorLockMode.None;
    }

}
