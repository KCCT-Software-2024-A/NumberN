using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterAndExitController1 : MonoBehaviour
{
    // 出口か入口かを判別するフラグ
    [SerializeField] public bool is_exit = false;

    // SceneManagerScriptの参照
    private SceneManagerScript sceneManagerScript;
    
    // SceneSettingsの参照
    private SceneSettings sceneSettings;

    private void Start()
    {
        // SceneManagerScriptとSceneSettingsのインスタンスを取得
        sceneManagerScript = FindObjectOfType<SceneManagerScript>();
        sceneSettings = FindObjectOfType<SceneSettings>();
        Debug.Log("EnterAndExitController1 initialized.");
    }

    private void OnTriggerEnter(Collider other)
    {
        // プレイヤー以外の接触や必要コンポーネントの未設定時は処理を終了
        if (!other.CompareTag("Player") || sceneManagerScript == null || sceneSettings == null)
        {
            Debug.LogWarning("Invalid trigger detected or missing components.");
            return;
        }

        Debug.Log("Player entered trigger zone.");

        // シーン進行カウントをインクリメント
        SceneSettings.scenesProgressed++;
        Debug.Log("Scenes progressed: " + SceneSettings.scenesProgressed);

        // エンディングへ進む条件を満たしているか確認
        if (SceneSettings.scenesProgressed >= sceneSettings.GetRequiredScenesToAdvance())
        {
            if ((is_exit && !sceneManagerScript.CheckAnomaly()) || (!is_exit && sceneManagerScript.CheckAnomaly()))
            {
                Debug.Log("Advancing to EndingScene.");
                SceneManager.LoadScene("EndingScene");
                return;
            }
        }

        // 異変の有無を確認
        bool hasAnomaly = sceneManagerScript.CheckAnomaly();
        Debug.Log("Anomaly detected: " + hasAnomaly);

        // 条件に基づきシーン遷移を制御
        if ((is_exit && hasAnomaly) || (!is_exit && !hasAnomaly))
        {
            int nextSceneID = (sceneManagerScript.GetSceneID() == 0) ? sceneSettings.GetRandomSceneID() : 0;
            Debug.Log("Transitioning to Scene_" + nextSceneID);
            SceneManager.LoadScene("Scene_" + nextSceneID);
            SceneSettings.scenesProgressed = 0;
        }
        else
        {
            Debug.Log("Transitioning to a random scene.");
            GoToRandomScene(sceneSettings.GetMaxSceneID());
        }
    }

    // ランダムで次のシーンに進む処理
    private void GoToRandomScene(int maxSceneID)
    {
        int currentSceneID = sceneManagerScript.GetSceneID();
        int nextSceneID;

        do
        {
            nextSceneID = UnityEngine.Random.Range(1, maxSceneID + 1);
        } while (nextSceneID == currentSceneID);

        Debug.Log("Randomly transitioning to Scene_" + nextSceneID);
        SceneManager.LoadScene("Scene_" + nextSceneID);
    }
}
