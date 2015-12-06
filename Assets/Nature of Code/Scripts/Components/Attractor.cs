using UnityEngine;
using System.Collections;

public class Attractor : MonoBehaviour 
{
    public static readonly float GRAVITATIONAL_CONSTANT = 0.4f;

    [Header("Physic Attributes")]
    public Vector2 Location;
    public float Mass = 20;

    [Header("Attractional Attributes")]
    public float MinimumObjectDistance = 1f;
    public float MaximumObjectDistance = 2f;

    protected Transform _Transform;

    // Use this for initialization
    void Start () 
	{
        _Transform = transform;
        _Transform.position = Location;
	}

    public void Draw()
    {
        _Transform.position = Location;
    }

    public Vector2 CalculateAttractionalForce(float mass, Vector2 position)
    {
        // Force direction
        Vector2 force = Location - position;
        float distance = force.magnitude;
        distance = Mathf.Clamp(distance, MinimumObjectDistance, MaximumObjectDistance);
        force.Normalize();

        // Force magnitude
        float strength = (GRAVITATIONAL_CONSTANT * Mass * mass) / (distance * distance);
        force *= strength;

        return force;
    }
}
