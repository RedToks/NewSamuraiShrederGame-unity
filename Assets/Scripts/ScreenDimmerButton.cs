using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScreenDimmerButton : MonoBehaviour
{
    private ScreenDimmer screenDimmer;
    private Image image;
    private ChangeSceneTrigger changeSceneTrigger;
    [SerializeField] private GameObject[] spritesToDestroy;

    private void Start()
    {
        screenDimmer = FindObjectOfType<ScreenDimmer>();
        image = GetComponent<Image>();
        changeSceneTrigger = FindObjectOfType<ChangeSceneTrigger>();
    }

    public void OnButtonClick()
    {
        screenDimmer.DimScreen();
        image.enabled = false;
        StartCoroutine(DestroyAfterUndim());
        changeSceneTrigger.ChangeScene();
    }

    private IEnumerator DestroyAfterUndim()
    {
        yield return new WaitForSeconds(screenDimmer.dimmingDuration);
        screenDimmer.UndimScreen();
        Destroy(gameObject);

        foreach (var sprite in spritesToDestroy)
        {
            Destroy(sprite);
        }
    }
}
