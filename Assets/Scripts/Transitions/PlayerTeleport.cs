using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerTeleport : MonoBehaviour
{
    [SerializeField] private string targetScene;
    [SerializeField] private Vector3 playerTeleportPosition;
    [SerializeField] private TextMeshProUGUI promptText;

    public event Action OnTeleportChanged;

    private bool isColliding = false;

    private void Start()
    {
        promptText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Singletone.Instance.gameObject)
        {
            isColliding = true;
            promptText.gameObject.SetActive(true);          
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == Singletone.Instance.gameObject)
        {
            isColliding = false;
            promptText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (isColliding && Input.GetKey(KeyCode.F))
        {
            Debug.Log("Я нахожусь тут");
            SavePlayerData();
            LoadScene(targetScene);
        }
    }

    private void SavePlayerData()
    {
        Singletone.Instance.SavePlayerData(playerTeleportPosition);
        OnTeleportChanged?.Invoke();
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == targetScene)
        {
            SetPlayerPosition();
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void SetPlayerPosition()
    {
        Vector3 savedPosition = Singletone.Instance.playerPosition;
        Singletone player = Singletone.Instance;

        if (player != null)
        {
            player.transform.position = savedPosition;
        }
        else
        {
            Debug.LogError("Player not found in the scene with tag 'Player'");
        }
    }

    public void ChangeTargetScene(string newTargetScene)
    {
        targetScene = newTargetScene;
    }
}
