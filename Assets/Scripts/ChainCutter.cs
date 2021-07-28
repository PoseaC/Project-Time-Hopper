using UnityEngine;

public class ChainCutter : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<HingeJoint2D>() != null)
            Destroy(collision.collider.gameObject);
    }
}
