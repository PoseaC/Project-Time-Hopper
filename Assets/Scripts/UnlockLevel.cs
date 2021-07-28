using UnityEngine;
using UnityEngine.UI;

public class UnlockLevel : MonoBehaviour
{
    public int thisLevel; // the level represented by this button
    Text lvlName; // name of the level
    Image lvlLocked; // locked image for locked levels
    Image[] images; // all the locked images
    void Start()
    {
        lvlName = GetComponentInChildren<Text>(); // get the text component in children
        images = GetComponentsInChildren<Image>(); // get all the images in children and then select the locked one
        // GetComponentInChildren<>() starts searching from the parent and returns the first component that meets the type specified, in this case it would
        // return the button sprite so we have to take an array of them and search for the one with a parent
        GetComponent<Button>().interactable = false;
        foreach (Image img in images)
        {
            if (img.transform.parent != null)
                lvlLocked = img;
        }
        if (thisLevel <= PlayerPrefs.GetInt("LevelsUnlocked", 2)) // we unlock only the levels previously played
        {
            GetComponent<Button>().interactable = true;
            lvlLocked.enabled = false;
            lvlName.enabled = true;
        }
    }
}
