using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tutorial : MonoBehaviour
{
    public Animator tutorialAnimator;
    public Animator slider;
    public Text tutorialText;
    public MovingObstacle platform;
    
    public string[] tutorials;
    public Vector2[] tutorialPositions;

    int currentTutorial = 0;
    int currentTutorialPosition = 0;
    private void Update()
    {
        tutorialText.text = tutorials[currentTutorial];
        transform.position = tutorialPositions[currentTutorialPosition];
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentTutorial == tutorialPositions.Length - 1)
        {
            platform.enabled = true;
            slider.enabled = true;
        }
        tutorialAnimator.SetBool("Tutorial", false);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        StartCoroutine(NextTutorial());
    }
    
    IEnumerator NextTutorial()
    {
        tutorialAnimator.SetBool("Tutorial", true);

        if (currentTutorialPosition < tutorialPositions.Length - 1)
            currentTutorialPosition++;

        yield return new WaitForSeconds(1);

        if (currentTutorial < tutorials.Length - 1)
            currentTutorial++;
    }
}
