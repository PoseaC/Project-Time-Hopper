using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOptions : MonoBehaviour
{
    Animator cameraAnimator; // refrence to the camera animator
    Animator doorAnimator; // refrence to the exit door animator
    public float delayLoadTime = 1; // the amount of time to wait for the animation to play before loading the next level
    private void Start()
    {
        cameraAnimator = FindObjectOfType<CameraFollow>().GetComponent<Animator>(); //
        doorAnimator = FindObjectOfType<CompleteLevel>().GetComponent<Animator>();  //
        doorAnimator.SetBool("isClosing", true);                                    // get references for the door and camera to play the transition animation
    }
    public void Continue()
    {
        if ((PlayerPrefs.GetInt("LevelsUnlocked") + 1) < SceneManager.sceneCountInBuildSettings)
            StartCoroutine(LoadLevel(PlayerPrefs.GetInt("LevelsUnlocked") + 1)); // load the next level if it exists
        else
            StartCoroutine(LoadLevel(PlayerPrefs.GetInt("LevelsUnlocked"))); // otherwise load the last level
    }
    public void Play(int levelIndex)
    {
        StartCoroutine(LoadLevel(levelIndex)); // load the specified level
    }
    public IEnumerator LoadLevel(int levelIndex) // elephant in the room, the coroutine
    {
        cameraAnimator.SetTrigger("EndLevel");    //
        doorAnimator.SetBool("isClosing", false); // play the transition animations
        yield return new WaitForSeconds(delayLoadTime); // this horrible atrocity of a syntax basically splits the function in two,
        // first part of the function (^ before this yield return bs) is called immediately like a normal function, and the second
        // is called like a separate function after delayLoadTime seconds, if you can't wrap your head around it, Invoke() and global
        // variables are a valid work-around
        SceneManager.LoadScene(levelIndex);
    }
}
