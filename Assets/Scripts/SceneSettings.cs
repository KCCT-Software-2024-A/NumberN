using UnityEngine;

public class SceneSettings : MonoBehaviour
{
    [SerializeField] private int maxSceneID = 4;
    [SerializeField] private int requiredScenesToAdvance = 3;
    
    public static int scenesProgressed = 0;
    public static SceneSettings Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            scenesProgressed = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetMaxSceneID() => maxSceneID;
    public int GetRequiredScenesToAdvance() => requiredScenesToAdvance;

    public int GetRandomSceneID()
    {
        return Random.Range(1, maxSceneID + 1);
    }
}
