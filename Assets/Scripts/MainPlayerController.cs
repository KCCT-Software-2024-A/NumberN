using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class MainPlayerController : MonoBehaviour
{
    public float rotationSpeed = 90f; // 回転速度
    public float moveSpeed = 0.1f; // 移動速度
    public float mouseSensitivity = 100f; // マウス感度
    float xRotation = 0f; // X軸の回転角度

    void FixedUpdate ()
    {
        // 左右キー入力に応じて移動方向を決定
        float moveDirectionLR = 0.0f;
        if (Input.GetKey(KeyCode.A)) { moveDirectionLR -= 1.0f; }
        if (Input.GetKey(KeyCode.D)) { moveDirectionLR += 1.0f; }

        // 前進・後退の移動方向を決定
        float moveDirectionFB = 0.0f;
        if (Input.GetKey(KeyCode.W)) { moveDirectionFB += 1.0f; }
        if (Input.GetKey(KeyCode.S)) { moveDirectionFB -= 1.0f; }

        // 左右方向への移動
        transform.Translate(Vector3.right * moveDirectionLR * moveSpeed);
        
        // 前進・後退の移動
        transform.Translate(Vector3.forward * moveDirectionFB * moveSpeed);
        
       //  // Rotation
       //  float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; // マウスのX軸移動量
       //
       //  xRotation -= mouseX;
       //  
       // transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); 
    }
}