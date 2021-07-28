using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour
{
    public CharacterController2D controller; // reference to a character controller that takes care of more complex functions
    public Animator animator; // reference to the player's animator

    float movement; // movement direction
    float tapCount;
    public float timeBetweenTaps = .25f;

    bool jump = false; // bool variable to transmit to the character controller when to jump
    [HideInInspector] public bool hasKey;
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (!EventSystem.current.IsPointerOverGameObject(0)) 
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                    tapCount++;

                Vector2 target = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

                if (target.x > transform.position.x)
                    movement = 1;
                else
                    movement = -1;

                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    movement = 0;
                    StartCoroutine(DoubleTap(timeBetweenTaps));
                } 
            }
            else if(Input.touchCount > 1)
                    if (!EventSystem.current.IsPointerOverGameObject(1))
                    {
                        if (Input.GetTouch(1).phase == TouchPhase.Began)
                            tapCount++;

                        Vector2 target = Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position);

                        if (target.x > transform.position.x)
                            movement = 1;
                        else
                            movement = -1;

                        if (Input.GetTouch(1).phase == TouchPhase.Ended)
                        {
                            movement = 0;
                            StartCoroutine(DoubleTap(timeBetweenTaps));
                        }
                    }
        }

        if (movement != 0)
            GetComponent<Rigidbody2D>().isKinematic = false;

        animator.SetFloat("Speed", Mathf.Abs(movement)); // play run animation 
    }

    IEnumerator DoubleTap(float tapDelay)
    {
        yield return new WaitForSeconds(tapDelay);

        if (tapCount >= 2)
        {
            jump = true;
            animator.SetBool("isJumping", true); //play jump animation
            GetComponent<Rigidbody2D>().isKinematic = false;
        }

        tapCount = 0;
    }

    public void OnLand()
    {
        animator.SetBool("isJumping", false); // upon landing transition to another animation
    }
    private void FixedUpdate()
    {
        controller.Move(movement, false, jump); // check for movement every physics update
        jump = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Death"))
            FindObjectOfType<GameManager>().gameHasEnded = true; // end the game if dead
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            transform.parent = null;
            GetComponent<Rigidbody2D>().isKinematic = false;
            transform.rotation = Quaternion.identity;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            hasKey = true;
        }
    }
}
