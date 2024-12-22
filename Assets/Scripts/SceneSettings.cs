using UnityEngine;

public class SceneSettings : MonoBehaviour
{
    // シーンの総数 (0 ～ maxSceneID の範囲)
    [SerializeField] public int maxSceneID = 4;

    // エンディングに進むための進行回数
    [SerializeField] public int requiredScenesToAdvance = 3;

    // このスクリプトを通して、他のスクリプトで設定を取得できるようにする
    public int GetMaxSceneID()
    {
        return maxSceneID;
    }

    public int GetRequiredScenesToAdvance()
    {
        return requiredScenesToAdvance;
    }
}
