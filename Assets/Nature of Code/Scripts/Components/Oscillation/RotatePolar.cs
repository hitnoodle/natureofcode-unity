using UnityEngine;
using System.Collections;

public class RotatePolar : MonoBehaviour
{
    public float Theta = 0f;
    public float Speed = Mathf.PI;

    protected Transform _Transform;
    protected float _Radius;

	// Use this for initialization
	void Start ()
    {
        _Transform = transform;
        _Radius = (_Transform.position).magnitude;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float x = _Radius * Mathf.Sin(Theta);
        float y = _Radius * Mathf.Cos(Theta);

        _Transform.position = new Vector2(x, y);
        Theta += Speed * Time.deltaTime;
	}
}
