using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerTeleport : MonoBehaviour
{
    [SerializeField] private string targetScene;
    [SerializeField] private Vector3 playerTeleportPosition;

    private bool isColliding = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == PlayerData.Instance.gameObject)
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == PlayerData.Instance.gameObject)
        {
            isColliding = false;
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
        PlayerData.Instance.SavePlayerData(playerTeleportPosition);
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
        Vector3 savedPosition = PlayerData.Instance.playerPosition;
        PlayerData player = PlayerData.Instance;

        if (player != null)
        {
            player.transform.position = savedPosition;
        }
        else
        {
            Debug.LogError("Player not found in the scene with tag 'Player'");
        }
    }
}
