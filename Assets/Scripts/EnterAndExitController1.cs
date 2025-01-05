using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterAndExitController1 : MonoBehaviour
{
    // 出口か入口かを判別するフラグ
    [SerializeField] public bool is_exit = false;

    // エンディングシーンかどうかを判別するフラグ
    [SerializeField] public bool isEndingScene = false;

    // SceneManagerScriptの参照
    private SceneManagerScript sceneManagerScript;
    private SceneSettings sceneSettings;

    private void Start()
    {
        // SceneManagerScriptとSceneSettingsのインスタンスを取得
        sceneManagerScript = FindObjectOfType<SceneManagerScript>();
        sceneSettings = FindObjectOfType<SceneSettings>();

        if (sceneManagerScript == null || sceneSettings == null)
        {
            Debug.LogError("SceneManagerScriptまたはSceneSettingsが見つかりません。");
        }
        else
        {
            Debug.Log("EnterAndExitController1 initialized.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger entered by: {other.name}");

        // プレイヤー以外の接触、または必要なコンポーネントが未設定の場合、処理を中断
        if (!other.CompareTag("Player"))
        {
            Debug.LogWarning("Trigger entered by non-Player object.");
            return;
        }
        if (sceneManagerScript == null || sceneSettings == null)
        {
            Debug.LogError("SceneManagerScriptまたはSceneSettingsが未設定です。");
            return;
        }

        Debug.Log("Player entered trigger zone.");

        // シーン進行カウントを増加
        SceneSettings.scenesProgressed++;
        Debug.Log("Scenes progressed: " + SceneSettings.scenesProgressed);

        // isEndingSceneの条件に基づいて遷移先を決定
        if (isEndingScene)
        {
            SceneSettings.scenesProgressed = 0;
            if (is_exit)
            {
                // isEndingSceneがtrueで、is_exitがtrueの場合、シーン0へ
                Debug.Log("Ending scene with exit. Transitioning to Scene 0.");
                SceneManager.LoadScene("Scene_0");
            }
            else
            {
                // isEndingSceneがtrueで、is_exitがfalseの場合、クリアシーンに遷移
                Debug.Log("Ending scene without exit. Transitioning to Clear Scene.");
                SceneManager.LoadScene("ClearScene");
            }
            return;
        }

        // エンディング条件の確認
        if (SceneSettings.scenesProgressed >= sceneSettings.GetRequiredScenesToAdvance())
        {
            Debug.Log("Checking ending conditions...");
            if ((is_exit && !sceneManagerScript.CheckAnomaly()) || (!is_exit && sceneManagerScript.CheckAnomaly()))
            {
                Debug.Log("Advancing to EndingScene.");
                SceneManager.LoadScene("EndingScene");
                return;
            }
            else
            {
                Debug.Log("Not advancing to EndingScene: Conditions not met.");
            }
        }

        // 異変の有無を確認
        bool hasAnomaly = sceneManagerScript.CheckAnomaly();
        Debug.Log("Anomaly detected: " + hasAnomaly);

        // 出口かつ異変あり、または入口かつ異変なしの場合のシーン遷移制御
        int nextSceneID;

        if ((is_exit && hasAnomaly) || (!is_exit && !hasAnomaly))
        {
            Debug.Log("Conditions met for controlled scene transition.");
            SceneSettings.scenesProgressed = 0;
            if (sceneManagerScript.GetSceneID() == 0)
            {
                // IDが0の場合は1以上のランダムシーンへ
                nextSceneID = UnityEngine.Random.Range(1, sceneSettings.GetMaxSceneID() + 1);
                Debug.Log("SceneID is 0. Transitioning to a random scene ID: " + nextSceneID);
            }
            else
            {
                // IDが0でない場合はID0に戻る
                nextSceneID = 0;
                Debug.Log("SceneID is not 0. Returning to Scene 0.");
            }

            Debug.Log("Transitioning to Scene_" + nextSceneID);
            SceneManager.LoadScene("Scene_" + nextSceneID);
        }
        else
        {
            // それ以外の場合はランダムシーンへ遷移
            Debug.Log("Conditions not met for controlled transition. Transitioning to a random scene.");
            GoToRandomScene(sceneSettings.GetMaxSceneID());
        }
    }

    // 現在のシーン以外のランダムシーンへ移動する処理
    private void GoToRandomScene(int maxSceneID)
    {
        int currentSceneID = sceneManagerScript.GetSceneID();
        int nextSceneID;

        Debug.Log("Starting random scene transition. Current scene ID: " + currentSceneID);

        do
        {
            nextSceneID = UnityEngine.Random.Range(1, maxSceneID + 1);
            Debug.Log("Randomly selected scene ID: " + nextSceneID);
        } while (nextSceneID == currentSceneID);

        Debug.Log("Transitioning to Random Scene_" + nextSceneID);
        SceneManager.LoadScene("Scene_" + nextSceneID);
    }
}
