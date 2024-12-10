using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f; // マウス感度

    float xRotation = 0f; // X軸の回転角度
    private float yRotation = 0f;

    [SerializeField] Transform playerBody;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.None; // カーソルのロックを解除
        Cursor.visible = true; // カーソルを表示する

        // カメラの初期回転角度を設定し、正面を向くようにする
        transform.localRotation = Quaternion.identity;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; // マウスのX軸移動量
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; // マウスのY軸移動量

        // プレイヤーのY軸（水平方向）を無制限に回転
        playerBody.Rotate(Vector3.up * mouseX);

        // カメラのX軸（垂直方向）の回転を制限
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 垂直方向の回転角度を制限
        yRotation += mouseX;

        // カメラの垂直方向の回転を反映
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}