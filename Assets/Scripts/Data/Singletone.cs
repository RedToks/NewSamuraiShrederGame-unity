using UnityEngine;

public class Singletone : MonoBehaviour
{
    public static Singletone Instance { get; private set; }

    public Vector3 playerPosition;
        
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SavePlayerData(Vector3 position)
    {
        playerPosition = position;
    }
}
