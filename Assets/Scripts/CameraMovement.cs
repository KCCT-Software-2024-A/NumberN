using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraControlScript : MonoBehaviour
{
    [SerializeField] private Transform[] cameraPositions;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float fadeDuration = 2f;
    [SerializeField] private Image fadeImage; // キャンバスのイメージを直接制御

    private int currentTargetIndex = 0;

    void Start()
    {
        fadeImage.color = new Color(0, 0, 0, 1); // 最初は暗転
        transform.position = cameraPositions[currentTargetIndex].position;
        transform.rotation = cameraPositions[currentTargetIndex].rotation;

        StartCoroutine(RotateAndFadeIn());
    }

    IEnumerator RotateAndFadeIn()
    {
        // 回転と明転を同時に行う
        float timer = 0f;
        while (timer < fadeDuration)
        {
            // 回転しながら明転
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(1f, 0f, timer / fadeDuration));
            timer += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 0); // 明転後、フェードは終了

        // 回転が完了したら次の動作へ
        StartCoroutine(RotateAndFadeOut());
    }

    IEnumerator RotateAndFadeOut()
    {
        // 80度回転しながら暗転
        float rotationTimer = 0f;
        float fadeTimer = 0f;

        while (rotationTimer < 80f / rotationSpeed)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(0f, 1f, fadeTimer / fadeDuration)); // 暗転の進行
            rotationTimer += rotationSpeed * Time.deltaTime;
            fadeTimer += Time.deltaTime;
            yield return null;
        }

        // 回転と暗転が完了したら、完全に暗転
        fadeImage.color = new Color(0, 0, 0, 1);

        // 次のカメラ位置に移動
        NextCameraPosition();
    }

    private void NextCameraPosition()
    {
        currentTargetIndex = (currentTargetIndex + 1) % cameraPositions.Length;
        transform.position = cameraPositions[currentTargetIndex].position;
        transform.rotation = cameraPositions[currentTargetIndex].rotation;

        // 明転しながら回転開始
        StartCoroutine(RotateAndFadeIn());
    }
}
