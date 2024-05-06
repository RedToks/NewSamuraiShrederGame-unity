using System.Collections;
using TMPro;
using UnityEngine;

public class TextAppearAnimation : MonoBehaviour
{
    private TMP_Text textComponent;
    [SerializeField] private float appearDurationPerCharacter = 0.03f; 

    private void Awake()
    {
        if (textComponent == null)
        {
            textComponent = GetComponent<TMP_Text>();
        }
    }

    public void PlayAnimation()
    {
        StartCoroutine(AppearAnimation());
    }

    private IEnumerator AppearAnimation()
    {
        textComponent.maxVisibleCharacters = 0;

        for (int i = 0; i <= textComponent.text.Length; i++)
        {
            textComponent.maxVisibleCharacters = i;
            yield return new WaitForSeconds(appearDurationPerCharacter);
        }
    }
}
