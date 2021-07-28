using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if ((SceneManager.GetActiveScene().buildIndex + 1) < SceneManager.sceneCountInBuildSettings)
                StartCoroutine(FindObjectOfType<GameOptions>().LoadLevel(SceneManager.GetActiveScene().buildIndex + 1)); // load the next level if it exists
            else
                StartCoroutine(FindObjectOfType<GameOptions>().LoadLevel(0)); // otherwise go back to menu

            if (SceneManager.GetActiveScene().buildIndex > PlayerPrefs.GetInt("LevelsUnlocked"))
                PlayerPrefs.SetInt("LevelsUnlocked", SceneManager.GetActiveScene().buildIndex); // if the player finished a new level save the progress
        }
    }
}
