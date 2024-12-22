using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterAndExitController1 : MonoBehaviour
{
    // 出口にあるか入口にあるか
    [SerializeField] public bool is_exit = false;

    // SceneManagerScriptの参照
    private SceneManagerScript sceneManagerScript;

    // 進んだシーン数
    private int scenesProgressed = 0;

    // SceneSettingsの参照
    private SceneSettings sceneSettings;

    private void Start()
    {
        // SceneManagerScriptがアタッチされたオブジェクトを探す
        sceneManagerScript = FindObjectOfType<SceneManagerScript>();

        // SceneSettingsがアタッチされたオブジェクトを探す
        sceneSettings = FindObjectOfType<SceneSettings>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (sceneManagerScript != null && sceneSettings != null)
            {
                // maxSceneIDとrequiredScenesToAdvanceをSceneSettingsから取得
                int maxSceneID = sceneSettings.GetMaxSceneID();
                int requiredScenesToAdvance = sceneSettings.GetRequiredScenesToAdvance();

                // 進んだシーン数をカウント
                scenesProgressed++;

                // シーン進行カウントが所定回数に達したかどうかをチェック
                if (scenesProgressed >= requiredScenesToAdvance)
                {
                    // エンディングに進む条件チェック
                    if (is_exit && !sceneManagerScript.CheckAnomaly())
                    {
                        // 出口で異変がない場合、エンディングに進む
                        SceneManager.LoadScene("EndingScene"); // エンディングシーンへ
                        return;
                    }
                    else if (!is_exit && sceneManagerScript.CheckAnomaly())
                    {
                        // 入口で異変がある場合、エンディングに進む
                        SceneManager.LoadScene("EndingScene"); // エンディングシーンへ
                        return;
                    }
                }

                // SceneManagerScriptからhasAnomalyの値を取得
                bool hasAnomaly = sceneManagerScript.CheckAnomaly();

                if (is_exit)
                {
                    // 出口で異変がある場合
                    if (hasAnomaly)
                    {
                        // 異変がある場合、最初のシーンに戻る
                        SceneManager.LoadScene("Scene_0");
                        scenesProgressed = 0;  // カウントリセット
                    }
                    else
                    {
                        // 異変がない場合、ランダムに次のシーンを選ぶ
                        GoToRandomScene(maxSceneID);
                    }
                }
                else
                {
                    // 入口で異変がある場合
                    if (hasAnomaly)
                    {
                        // 異変がある場合はランダムに次のシーンを選ぶ
                        GoToRandomScene(maxSceneID);
                    }
                    else
                    {
                        // 異変がない場合、最初のシーンに戻る
                        SceneManager.LoadScene("Scene_0");
                        scenesProgressed = 0;  // 最初のシーンに戻った場合、カウントリセット
                    }
                }
            }
        }
    }

    // ランダムな次のシーンに進む
    private void GoToRandomScene(int maxSceneID)
    {
        if (sceneManagerScript != null)
        {
            // SceneManagerScriptからsceneIDを取得
            int currentSceneID = sceneManagerScript.GetSceneID();
            int nextSceneID;

            do
            {
                // 現在のシーンID以外をランダムに選択
                nextSceneID = UnityEngine.Random.Range(1, maxSceneID + 1);  // 0を省いてランダムに選択
            } while (nextSceneID == currentSceneID);

            // シーン名を取得してロード
            string sceneName = "Scene_" + nextSceneID;  // 例: Scene_1, Scene_2, ...
            SceneManager.LoadScene(sceneName);
        }
    }

    void Update()
    {
    }
}
