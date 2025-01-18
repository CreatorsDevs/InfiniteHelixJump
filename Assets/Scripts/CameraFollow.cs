using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed;

    private float currentY;

    private void Start()
    {
        currentY = player.position.y;
    }
    private void LateUpdate()
    {
        if (player.position.y < currentY)
        {
            Vector3 targetPosition = new(
                transform.position.x,
                player.position.y + offset.y,
                transform.position.z
            );

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
            currentY = player.position.y;
        }
    }
}
