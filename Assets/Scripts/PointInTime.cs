using UnityEngine;

public class PointInTime // class used to save positions and rotations to play in reverse for a rewind effect
{
    public Vector3 position;
    public Quaternion rotation;

    public PointInTime (Vector3 _position, Quaternion _rotation)
    {
        position = _position;
        rotation = _rotation;
    }
}
