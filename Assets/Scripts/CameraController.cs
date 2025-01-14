using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f; // マウス感度

    float xRotation = 0f; // X軸の回転角度
    private float yRotation = 0f;

    [SerializeField] Transform playerBody;
    
    void Start()
    {
        UpdateCursorState();
        transform.localRotation = Quaternion.identity;
    }

    void Update()
    {
        UpdateCursorState();

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            playerBody.Rotate(Vector3.up * mouseX);

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            yRotation += mouseX;

            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }
    }

    void UpdateCursorState()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "title" || sceneName == "ClearScene")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
