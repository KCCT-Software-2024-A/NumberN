using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;  // ターゲットへの参照
    private Vector3 offset;  // 相対座標

    void Start ()
    {
        // 自分自身とtargetとの相対座標を求める
        offset = transform.position - target.position;
        // Y軸のオフセットを設定（例：Y軸を1だけ上げる）
        offset.y = 1.0f;
    }

    void Update ()
    {
        // 自分自身の座標に、targetの座標に相対座標を足した値を設定する
        transform.position = target.position + offset;
    }
}