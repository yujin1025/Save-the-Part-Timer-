using UnityEngine;
using UnityEngine.UI; // UI 컴포넌트를 사용하기 위해 추가
using System.Collections;

public class BlinkingImage : MonoBehaviour
{
    public Image imageToBlink; // 깜빡이게 할 이미지
    public float blinkDuration; // 깜빡임 사이의 시간 간격

    // Use this for initialization
    void Start()
    {
        blinkDuration = 0.3f;
    }

    private void OnEnable()
    {
        imageToBlink = gameObject.GetComponent<Image>();
        // 깜빡임 코루틴 시작
        StartCoroutine(BlinkImage());
    }

    IEnumerator BlinkImage()
    {
        // 무한 루프
        while (true)
        {
            // 이미지를 서서히 불투명하게 만듭니다.
            yield return StartCoroutine(FadeImage(true));
            // 잠시 대기
            yield return new WaitForSeconds(blinkDuration);
            // 이미지를 서서히 투명하게 만듭니다.
            yield return StartCoroutine(FadeImage(false));
            // 잠시 대기
            yield return new WaitForSeconds(blinkDuration);
        }
    }

    IEnumerator FadeImage(bool fadeIn)
    {
        // fade in이면 목표 알파 값을 1로, fade out이면 0으로 설정합니다.
        float targetAlpha = fadeIn ? 1.0f : 0.0f;
        // 현재 알파와 목표 알파 사이를 보간하는 동안 반복합니다.
        while (!Mathf.Approximately(imageToBlink.color.a, targetAlpha))
        {
            // 새 알파 값을 계산합니다.
            float newAlpha = Mathf.MoveTowards(imageToBlink.color.a, targetAlpha, Time.deltaTime / blinkDuration);
            // 이미지의 색상에 새 알파 값을 설정합니다.
            imageToBlink.color = new Color(imageToBlink.color.r, imageToBlink.color.g, imageToBlink.color.b, newAlpha);
            yield return null;
        }
    }
}
