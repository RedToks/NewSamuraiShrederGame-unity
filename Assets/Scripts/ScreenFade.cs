using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    [SerializeField] private Image blackoutImage;
    [SerializeField] private TextMeshProUGUI fadeInText;
    [SerializeField] private Transform objectToDestroy;

    private PlayerControl playerControl;

    [SerializeField] private float delayBeforeFade = 2f;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float fadeTextDuration = 0.5f;
    [SerializeField] private float targetAlpha = 1f;
    [SerializeField] private float delayBeforeFadeOut = 5f; 
    [SerializeField] private float delayBeforeTextFadeIn = 2f; 

    private bool _isFading = false;

    private void Start()
    {
        fadeInText.alpha = 0f;

        playerControl = FindObjectOfType<PlayerControl>();
    }

    public void Fade()
    {
        if (_isFading)
            return;

        StartCoroutine(FadeCoroutine());
    }

    private IEnumerator FadeCoroutine()
    {

        if (playerControl != null)
            playerControl.EnableControl(false);

        _isFading = true;


        yield return new WaitForSeconds(delayBeforeFade);

        float timer = 0f;

        Color startColor = blackoutImage.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);



        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            blackoutImage.color = Color.Lerp(startColor, targetColor, timer / fadeDuration);
            yield return null;
        }

        blackoutImage.color = targetColor;

        if (Mathf.Approximately(blackoutImage.color.a, targetAlpha))
        {
            yield return new WaitForSeconds(delayBeforeTextFadeIn);
            StartCoroutine(FadeTextCoroutine());
        }
    }

    private IEnumerator FadeTextCoroutine()
    {
        float timer = 0f;

        Color startTextColor = fadeInText.color;
        Color targetTextColor = new Color(startTextColor.r, startTextColor.g, startTextColor.b, 1f);

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeInText.color = Color.Lerp(startTextColor, targetTextColor, timer / fadeTextDuration);
            yield return null;
        }

        fadeInText.color = targetTextColor;

        if (Mathf.Approximately(fadeInText.color.a, 1f))
        {
            if (objectToDestroy != null)
                Destroy(objectToDestroy.gameObject);

            StartCoroutine(FadeOutCoroutine());
        }
    }

    private IEnumerator FadeOutCoroutine()
    {
        yield return new WaitForSeconds(delayBeforeFadeOut);

        float timer = 0f;

        Color startColor = blackoutImage.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            blackoutImage.color = Color.Lerp(startColor, targetColor, timer / fadeDuration);
            fadeInText.color = Color.Lerp(startColor, targetColor, timer / fadeTextDuration);
            yield return null;
        }

        blackoutImage.color = targetColor;

        if (playerControl != null)
            playerControl.EnableControl(true);

        _isFading = false;
    }
}
