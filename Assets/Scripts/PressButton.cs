using UnityEngine;

public class PressButton : MonoBehaviour
{
    public float offset;
    public Animator animator;
    public BoxCollider2D collider;
    public MovingObstacle[] triggeredObjects;
    private void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.transform.position.y + offset > transform.position.y)
        {
            animator.enabled = true;
            collider.offset = new Vector2(0, -.25f);
            collider.size = new Vector2(1.75f, .5f);
            foreach (MovingObstacle obstacle in triggeredObjects)
            {
                obstacle.enabled = true;
            }
        }
    }
}
