using UnityEngine;
using UnityEngine.UI;  // Unityの標準UIを使用するためにインポート

public class SceneProgressDisplay : MonoBehaviour
{
    // Text型を使用
    [SerializeField] private Text progressText;

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
            progressText.text = $"{SceneSettings.scenesProgressed} / {sceneSettings.GetRequiredScenesToAdvance()+1}";
        }
    }
}
