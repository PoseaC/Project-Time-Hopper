using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float cameraSpeed;
    public Vector3 cameraOffset;
    Transform player;
    private void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>().transform;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position + cameraOffset, cameraSpeed); // move the camera towards the player
    }
}
