using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
    [Header("Physic Attributes")]
    public Vector2 Location;
    public Vector2 Velocity;
    public Vector2 Acceleration;

    [Header("Attractional Attributes")]
    public float MinimumObjectDistance = 1f;
    public float MaximumObjectDistance = 2f;

    public float Mass = 1;

    protected Transform _Transform;
    protected Bounds _CameraBounds;

    // Constructor
    public void Initialize(float mass, Vector2 location)
    {
        Mass = mass;
        Location = location;
    }

	// Use this for initialization
	void Start ()
    {
        _CameraBounds = CameraExtensions.OrthographicBounds(Camera.main);

        _Transform = transform;
        _Transform.position = Location;
        _Transform.localScale = Vector3.one * Mass;
    }

    #region Update

    public void UpdatePhysics()
    {
        Velocity += Acceleration * Time.deltaTime;
        Location += Velocity * Time.deltaTime;

        Acceleration *= 0;
    }

    public void CheckEdges()
    {
        if (Location.x > _CameraBounds.extents.x)
        {
            Location.x = _CameraBounds.extents.x;
            Velocity.x *= -1;
        }
        else if (Location.x < -_CameraBounds.extents.x)
        {
            Location.x = -_CameraBounds.extents.x;
            Velocity.x *= -1;
        }

        if (Location.y > _CameraBounds.extents.y)
        {
            Location.y = _CameraBounds.extents.y;
            Velocity.y *= -1;
        }
        else if (Location.y < -_CameraBounds.extents.y)
        {
            Location.y = -_CameraBounds.extents.y;
            Velocity.y *= -1;
        }
    }

    public void Draw()
    {
        _Transform.position = Location;
    }

    #endregion

    #region Public Functions

    // Start is bottom left
    public bool IsOnLocation(Vector2 start, Vector2 area)
    {
        float x = _Transform.position.x;
        float y = _Transform.position.y;

        return (x > start.x && x < start.x + area.x && y > start.y && y < start.y + area.y);
    }

    public void ApplyForce(Vector2 force)
    {
        Vector2 f = force / Mass;
        Acceleration += f;
    }

    public void ApplyDragForce(float dragCoefficient)
    {
        // Force's magnitude: Cd * (v ^ 2)
        float speed = Velocity.magnitude;
        float dragMagnitude = dragCoefficient * speed * speed; 

        // Force's direction: -1 * velocity
        Vector2 drag = Velocity;
        drag *= -1;
        drag.Normalize();

        // Force final: magnitude * direction
        drag *= dragMagnitude; 

        // Done, apply it
        ApplyForce(drag);
    }

    public Vector2 CalculateAttractionalForce(float mass, Vector2 position)
    {
        // Force direction
        Vector2 force = Location - position;
        float distance = force.magnitude;
        distance = Mathf.Clamp(distance, MinimumObjectDistance, MaximumObjectDistance);
        force.Normalize();

        // Force magnitude
        float strength = (Attractor.GRAVITATIONAL_CONSTANT * Mass * mass) / (distance * distance);
        force *= strength;

        return force;
    }

    #endregion
}
