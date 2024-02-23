using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    private string originalText;
    private string currentText = "";
    private float textSpeed = 0.05f;
    private bool isAnimating = false;
    private Coroutine animationCoroutine;

    private void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        originalText = textComponent.text;
        textComponent.text = "";
    }

    public void StartAnimation()
    {
        if (!isAnimating)
        {
            isAnimating = true;
            animationCoroutine = StartCoroutine(AnimateText()); 
        }
    }

    public void StopAnimation()
    {
        if (isAnimating)
        {
            isAnimating = false;
            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine); // Останавливаем корутину
                animationCoroutine = null; // Сбрасываем ссылку
            }
        }
    }

    IEnumerator AnimateText()
    {
        for (int i = 0; i < originalText.Length; i++)
        {
            currentText += originalText[i];
            textComponent.text = currentText;
            yield return new WaitForSeconds(textSpeed);
        }

        isAnimating = false;
    }
}
