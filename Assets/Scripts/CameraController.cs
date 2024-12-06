using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f; // マウス感度

    float xRotation = 0f; // X軸の回転角度

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

        xRotation -= mouseY; // マウスのY軸移動量に応じてX軸の回転角度を更新し、上下の回転を制限
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 上下の回転を制限する

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // カメラのX軸の回転を設定
        transform.parent.Rotate(Vector3.up * mouseX); // マウスのX軸移動量に応じてプレイヤー（またはカメラの親オブジェクト）を回転させる
    }
}