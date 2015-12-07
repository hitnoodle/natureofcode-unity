using UnityEngine;
using System.Collections;

public class FollowMouse : MonoBehaviour
{
    public Vector2 Location;
    public Vector2 Velocity;
    public Vector2 Acceleration;

    protected Transform _Transform;

    // Use this for initialization
    void Start ()
    {
        _Transform = transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mousePos - new Vector2(_Transform.position.x, _Transform.position.y);
        dir.Normalize();
        dir *= 0.5f;
        Acceleration = dir;

        Velocity += Acceleration * Time.deltaTime;
        Location += Velocity * Time.deltaTime;

        Acceleration *= 0;

        float angle = Mathf.Atan2(Velocity.x, Velocity.y);

        _Transform.Rotate(new Vector3(0, 0, angle));
        _Transform.position = Location;
    }
}
