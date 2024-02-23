using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenDimmer : MonoBehaviour
{
    public float dimmingSpeed = 1.0f;
    public float dimmingDuration = 10.0f;
    public float textDuration = 5.0f;

    [SerializeField] private TextMeshProUGUI sleepingText;
    private Image dimmerImage;
    private bool isDimming = false;

    private float currentAlpha = 0f;
    private float targetAlpha = 0f;

    private void Start()
    {
        dimmerImage = GetComponent<Image>();
        sleepingText = FindObjectOfType<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (isDimming)
        {
            currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, dimmingSpeed * Time.deltaTime);
            dimmerImage.color = new Color(dimmerImage.color.r, dimmerImage.color.g, dimmerImage.color.b, currentAlpha);
            sleepingText.color = new Color(sleepingText.color.r, sleepingText.color.g, sleepingText.color.b, currentAlpha);

            if (currentAlpha == targetAlpha)
            {
                isDimming = false;
            }
        }
    }

    public void DimScreen()
    {
        isDimming = true;
        targetAlpha = 1f;
        Player.Instance.DisablePlayerControl();
    }

    public void UndimScreen()
    {
        isDimming = true;
        targetAlpha = 0f;
        Player.Instance.EnablePlayerControl();
    }
}
