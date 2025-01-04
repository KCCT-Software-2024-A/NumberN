using UnityEngine;

public class SceneSettings : MonoBehaviour
{
    // シーンの総数 (0 ～ maxSceneID の範囲)
    [SerializeField] public int maxSceneID = 4;

    // エンディングに進むための進行回数
    [SerializeField] public int requiredScenesToAdvance = 3;

    // 進行したシーンの数を保持する静的変数
    public static int scenesProgressed = 0;

    // このスクリプトを通して、他のスクリプトで設定を取得できるようにする
    public int GetMaxSceneID()
    {
        return maxSceneID;
    }

    public int GetRequiredScenesToAdvance()
    {
        return requiredScenesToAdvance;
    }

    // シーンIDが0の場合、ランダムで1から最大のところに飛ぶ
    public int GetRandomSceneID()
    {
        return Random.Range(1, maxSceneID + 1);
    }
}
