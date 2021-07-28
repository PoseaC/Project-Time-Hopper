using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{// same logic as other game managers, keep track of a variable that ends the game
    public bool gameHasEnded = false; 
    public float thisStageRecordTime;
    public Text cooldownText;
    public Text currentLevel;
    private void Start()
    {
        currentLevel.text = (SceneManager.GetActiveScene().buildIndex - 1).ToString();
    }
    void Update()
    {
        if (gameHasEnded)
            EndGame();
    }
    void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
