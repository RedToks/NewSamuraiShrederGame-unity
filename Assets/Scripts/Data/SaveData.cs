using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData Instance { get; private set; }

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

    public void SaveCameraPosition(Vector3 position)
    {
        PlayerPrefs.SetFloat("CameraPosX", position.x);
        PlayerPrefs.SetFloat("CameraPosY", position.y);
        PlayerPrefs.SetFloat("CameraPosZ", position.z);
        PlayerPrefs.Save();
    }

    public Vector3 LoadCameraPosition(Vector3 defaultPosition)
    {
        float x = PlayerPrefs.GetFloat("CameraPosX", defaultPosition.x);
        float y = PlayerPrefs.GetFloat("CameraPosY", defaultPosition.y);
        float z = PlayerPrefs.GetFloat("CameraPosZ", defaultPosition.z);
        return new Vector3(x, y, z);
    }

    public void SavePlayerPosition(Vector3 position)
    {
        PlayerPrefs.SetFloat("PlayerPosX", position.x);
        PlayerPrefs.SetFloat("PlayerPosY", position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", position.z);
        PlayerPrefs.Save();
    }

    public Vector3 LoadPlayerPosition(Vector3 defaultPosition)
    {
        float x = PlayerPrefs.GetFloat("PlayerPosX", defaultPosition.x);
        float y = PlayerPrefs.GetFloat("PlayerPosY", defaultPosition.y);
        float z = PlayerPrefs.GetFloat("PlayerPosZ", defaultPosition.z);
        return new Vector3(x, y, z);
    }
}
