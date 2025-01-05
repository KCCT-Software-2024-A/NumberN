using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFlipOnTrigger : MonoBehaviour
{
    // 反転させたいオブジェクト
    [SerializeField] private Transform targetObject;

    // 初期の回転を保存
    private Quaternion originalRotation;

    private void Start()
    {
        // 初期の回転を保存
        if (targetObject != null)
        {
            originalRotation = targetObject.rotation;
        }
    }

    // トリガーに入ったときに呼ばれる
    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーが触れた場合に反転
        if (other.CompareTag("Player"))
        {
            FlipObject();
        }
    }

    // トリガーから出たときに呼ばれる
    private void OnTriggerExit(Collider other)
    {
        // プレイヤーが出た場合に回転を元に戻す
        if (other.CompareTag("Player"))
        {
            ResetObjectRotation();
        }
    }

    // オブジェクトを反転させる処理
    private void FlipObject()
    {
        if (targetObject != null)
        {
            targetObject.rotation = Quaternion.Euler(0, 85, 0);  // Y軸回転で反転
            Debug.Log("Object flipped.");
        }
    }

    // オブジェクトの回転を元に戻す処理
    private void ResetObjectRotation()
    {
        if (targetObject != null && targetObject.rotation != originalRotation)
        {
            targetObject.rotation = originalRotation;  // 初期の回転に戻す
            Debug.Log("Object rotation reset.");
        }
    }
}