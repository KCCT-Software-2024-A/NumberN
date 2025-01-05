using UnityEngine;
using TMPro;  // TextMeshProを使用するためにインポート

public class SceneProgressDisplay : MonoBehaviour
{
    // TextMeshProUGUI型を使用
    [SerializeField] private TextMeshProUGUI progressText;

    private SceneSettings sceneSettings;

    private void Start()
    {
        sceneSettings = SceneSettings.Instance;
        if (sceneSettings == null || progressText == null)
        {
            Debug.LogError("SceneSettingsまたはProgress Textが設定されていません。");
            return;
        }
        UpdateProgressText();
    }

    private void Update()
    {
        UpdateProgressText();
    }

    private void UpdateProgressText()
    {
        if (sceneSettings != null)
        {
            progressText.text = $"{SceneSettings.scenesProgressed} / {sceneSettings.GetMaxSceneID()+1}";
        }
    }
}
