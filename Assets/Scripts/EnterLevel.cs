using UnityEngine;

public class EnterLevel : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("isClosing", true); // play the closing door animation when the player leaves, purely stylistic
    }
}
