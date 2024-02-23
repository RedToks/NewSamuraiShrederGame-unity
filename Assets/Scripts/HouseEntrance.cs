using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class HouseEntrance : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    [SerializeField] private float activationDistance = 3f;
    public string sceneToLoad;
    [SerializeField] private Vector2 spawnPosition;
    [SerializeField] private Vector3 playerScale;
    private float currentCheckRadius;
    private ScreenFader screenFader;


    private void Start()
    {
        screenFader = GetComponentInChildren<ScreenFader>();
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.enabled = false;
    }

    private void Update()
    {

        float distance = Vector3.Distance(transform.position, Player.Instance.transform.position);

        if (distance <= activationDistance)
            textMesh.enabled = true;
        else
            textMesh.enabled = false;

        if (Input.GetKeyDown(KeyCode.F) && textMesh.enabled && Player.Instance.isPlayerControlEnabled)
        {
            screenFader.StartFadeIn();
            StartCoroutine(LoadNextScene());
        }
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(sceneToLoad);

        screenFader.StartFadeOut();

        Player.Instance.checkRadius = currentCheckRadius + 0.5f;
        Player.Instance.transform.position = spawnPosition;
        Player.Instance.transform.localScale = playerScale;
    }
}

