using UnityEngine;

public class PointLightFollowCamera : MonoBehaviour
{
    private Transform cameraTransform;

    void Start()
    {
        // メインカメラを取得
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // ポイントライトの位置をカメラの位置に追従
        transform.position = cameraTransform.position;
    }
}
