using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private float posY;
    [SerializeField] private float minY = -2.0f;
    [SerializeField] private float maxY = 2.0f;
    private readonly float posZ = -10;

    private Vector3 pos;

    private void Start()
    {
        SaveLoadData.Instance.SaveCameraPosition(pos);
        Vector3 savedCameraPosition = SaveLoadData.Instance.LoadCameraPosition(transform.position);
        transform.position = savedCameraPosition;
    }

    private void Update()
    {
        if (Singletone.Instance != null)
        {
            pos = Singletone.Instance.transform.position;
            pos.z = posZ;
            pos.y = Mathf.Clamp(pos.y + posY, minY, maxY);
            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * smoothSpeed);
        }
    }

    private void OnDestroy()
    {
        if (SaveLoadData.Instance != null)
        {
            SaveLoadData.Instance.SaveCameraPosition(transform.position);
        }
    }
}
