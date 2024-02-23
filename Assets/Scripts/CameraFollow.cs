using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private float posY;
    [SerializeField] private float minY = -2.0f; 
    [SerializeField] private float maxY = 2.0f;
    private readonly float posZ = -10;

    private Vector3 pos;


    private void Update()
    {
        if (!player)
            player = Player.Instance.transform;

        pos = player.position;
        pos.z = posZ;
        pos.y = Mathf.Clamp(pos.y + posY, minY, maxY);
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * smoothSpeed);
    }
}
