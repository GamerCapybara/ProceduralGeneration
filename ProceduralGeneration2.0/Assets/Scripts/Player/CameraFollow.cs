using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        if (player is null) return;

        Vector3 target = new Vector3(player.position.x, player.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, target, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
