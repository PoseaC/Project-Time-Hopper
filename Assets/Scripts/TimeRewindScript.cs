using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TimeRewindScript : MonoBehaviour
{
    bool isRewinding = false; // a bool that decides whether we should rewind or record information
    public bool shouldBeKinematic = false; // a bool to differentiate the object that should be kinematic at the end of the rewind(i.e. platforms)
    public RewindEffect rewindEffect; // a post-processing animation that should play to emphasize that time is rewinding

    public static float recordTime = 5f; // the amount of time the player can rewind in a level, without a limit there would be stupid memory leaks
    public static Text cooldown; // a text to display the timer to the player
    public static Slider rewindSlider;
    float curentRecordedTime = 0f; // a variable to keep track of how much time can be rewinded at the moment

    Rigidbody2D rb; // a reference to the rigidbody attached to the object

    List<PointInTime> pointsInTime; // a list of points that we use for the rewind
    void Start()
    {
        pointsInTime = new List<PointInTime>();                            //
        rb = GetComponent<Rigidbody2D>();                                  //
        rewindEffect = FindObjectOfType<RewindEffect>();                   //
        cooldown = FindObjectOfType<GameManager>().cooldownText;           //
        rewindSlider = FindObjectOfType<Slider>();                         //
        recordTime = FindObjectOfType<GameManager>().thisStageRecordTime;  // get the neccesary references before anything else
        rewindSlider.maxValue = recordTime / .02f;
    }

    void Update()
    {
        if (isRewinding) // decide if we play the post-processing effect 
            rewindEffect.DoRewindEffect();
        else
            rewindEffect.StopRewindEffect();

        if (!EventSystem.current.IsPointerOverGameObject(0))
        {
            isRewinding = false;
            rewindSlider.value = Mathf.Round(curentRecordedTime / .02f);
        }
        else
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                RemoveFuture(Mathf.RoundToInt(rewindSlider.value));
                rewindSlider.value = Mathf.Round(curentRecordedTime / .02f);
            }

            isRewinding = true;
        }

        cooldown.text = curentRecordedTime.ToString("0.00"); // display how much time the player can currently rewind
    }
    private void FixedUpdate()
    {
        if (isRewinding) // decide if we start rewinding or if we record information for later
        { // this is done in the FixedUpdate because messing with physics in Update is bad practice and can cause troubles
            Rewind(Mathf.RoundToInt(rewindSlider.value));
            curentRecordedTime = rewindSlider.value / 50;
        }
        else
        {
            if (curentRecordedTime < recordTime)
                curentRecordedTime += Time.fixedDeltaTime; //increase the timer while recording

            Record();
        }
    }
    public void StartRewind()
    {
        isRewinding = true;
        rb.isKinematic = true; // make all objects kinematic while rewinding so unity doesn't apply physics over them and cause lagg

        if (GetComponent<MovingObstacle>() != null)
            GetComponent<MovingObstacle>().enabled = false;
    }
    public void StopRewind()
    {
        isRewinding = false;

        if (GetComponent<MovingObstacle>() != null)
            GetComponent<MovingObstacle>().enabled = true;

        if (!shouldBeKinematic)
            rb.isKinematic = false; // change objects back to dynamic only if they should be (objects moved through script should remain kinematic)
    }

    void Record()
    {
        if (pointsInTime.Count > Mathf.Round(recordTime / .02f)) // physics updates run at exactly .02 seconds from one another, divide 1 by that and 
            // you get the amount of updates in a second, divide recordTime by that and you get the maximum amount of PointsInTime we can record
            pointsInTime.RemoveAt(0); // if we overcome that amount we start removing points from the end of the list

        pointsInTime.Insert(pointsInTime.Count, new PointInTime(transform.position, transform.rotation)); //add a position and a rotation at the start of the list
    }

    void Rewind(int spacePoint)
    {
        if (spacePoint > pointsInTime.Count)
        {
            transform.position = pointsInTime[pointsInTime.Count - 1].position; //
            transform.rotation = pointsInTime[pointsInTime.Count - 1].rotation; //
            rb.velocity = new Vector2(0, 0);
        }
        else if (spacePoint < 0)
        {
            transform.position = pointsInTime[0].position; //
            transform.rotation = pointsInTime[0].rotation; //
            rb.velocity = new Vector2(0, 0);
        }
        else
        {
            transform.position = pointsInTime[spacePoint].position; //
            transform.rotation = pointsInTime[spacePoint].rotation; //
            rb.velocity = new Vector2(0, 0);                        // move the object to that position and rotation
        }
    }

    void RemoveFuture(int spacePoint)
    {
        pointsInTime.RemoveRange(spacePoint, pointsInTime.Count - spacePoint);
    }
}
