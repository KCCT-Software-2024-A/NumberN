using UnityEngine;

public class ExitOnEscape : MonoBehaviour
{
    void Update()
    {
        // エスケープキーを押したらゲームを終了
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}
