using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    // このシーンの固有番号（手動で設定）
    [SerializeField]
    private int sceneID;

    // 異変があるかどうか（手動で設定）
    [SerializeField]
    private bool hasAnomaly;

    void Start()
    {

    }

    // シーンIDを取得する
    public int GetSceneID()
    {
        return sceneID;
    }

    // このシーンに異変があるかを取得する
    public bool CheckAnomaly()
    {
        return hasAnomaly;
    }
}