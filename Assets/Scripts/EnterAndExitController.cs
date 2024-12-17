using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterAndExitController : MonoBehaviour
{
    // 出口にあるか入口にあるか
    [SerializeField]
    public bool is_exit = false;
    // エンディングに行くか
    [SerializeField] public bool is_ending = false;

    // 次のシーン名(間違えてたらid=0, 合ってれば次のid)
    [SerializeField] public String nextSceneName = "";
    // 戻ったときのシーン名(ステージに間違いがあれば次のid, 間違いがなければid=0)
    [SerializeField] public String previousSceneName = "";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && is_exit)
        {
            // TODO: 出口のシーン名の取得をどうにかする
            SceneManager.LoadScene(nextSceneName);
        } else if (other.CompareTag("Player"))
        {
            // TODO: 入口のシーン名の取得をどうにかする
            SceneManager.LoadScene(previousSceneName);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
