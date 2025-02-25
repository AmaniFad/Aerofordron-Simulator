using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration;

    public void FadeInCoroutine()
    {
        StartCoroutine(FadeIn());
    }
    public void FadeOutCoroutine()
    {
        StartCoroutine(FadeOut());
    }
    private IEnumerator FadeIn()
    {
        yield return Fade(0, 1);
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(2f);
        yield return Fade(1, 0);
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = endAlpha;
    }
}
