using UnityEngine;
public class MovingObstacle : MonoBehaviour
{
    Rigidbody2D obstacle; // the obstacle's rigidbody

    Vector2 startingPoint; // the starting position of the object
    Vector2 endPoint; // the end position of the object

    public Vector2 distanceToMove; // how much the obstacle will move relative to the starting position
    [Range(0, .3f)] [SerializeField] public float movingSpeed; // the speed of movement
    [HideInInspector] public float initialSpeed; // the starting moving speed used to undo the slowmotion effect

    public bool oneWayTrip = false; // if the object should move continously between the starting and end point
    bool switchDirection = false; // when to switch the direction of movement
    void Start()
    {
        initialSpeed = movingSpeed;             //
        obstacle = GetComponent<Rigidbody2D>(); // get a reference to the rigidbody and the starting speed
        startingPoint = obstacle.position;      //
        endPoint = startingPoint + distanceToMove;//set the starting and end point relative to where the object started
    }

    void FixedUpdate()
    {
        if (!switchDirection) // while we haven't reached the end position
        {
            if (obstacle.position == endPoint && !oneWayTrip) // if we reached the end point and we shouldn't stop moving, we turn around towards the starting point
                switchDirection = true;
            obstacle.position = Vector2.MoveTowards(obstacle.position, endPoint, movingSpeed); // move towards the end position 
        }
        else
        {
            if (obstacle.position == startingPoint)
                switchDirection = false;
            obstacle.position = Vector2.MoveTowards(obstacle.position, startingPoint, movingSpeed);
        }
    }
}
