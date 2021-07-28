using UnityEngine;
using UnityEngine.U2D;
using System.Collections;

public class UnlockBarrier : MonoBehaviour
{
    public Animator animator;
    public SpriteShapeController controller;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (collision.collider.GetComponent<PlayerBehaviour>().hasKey)
            {
                collision.collider.GetComponent<PlayerBehaviour>().hasKey = false;
                StartCoroutine(Unlock());
            }
            else
                animator.SetTrigger("Lock");
        }
    }
    IEnumerator Unlock()
    {
        animator.SetTrigger("Unlock");
        yield return new WaitForSeconds(1);
        GetComponent<Collider2D>().enabled = false;
        while(controller.spline.GetPosition(0).y < 4)
        {
            controller.spline.SetPosition(0, new Vector3(0, controller.spline.GetPosition(0).y + .1f));
            yield return null;
        }
    }
}
