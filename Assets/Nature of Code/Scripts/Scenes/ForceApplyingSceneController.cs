using UnityEngine;
using System.Collections;

public class ForceApplyingSceneController : MonoBehaviour 
{
    [Header("Object Preparations")]
    public GameObject MoverPrefab;
    public int Number = 10;

    public float MinimumMass = 0.1f;
    public float MaximumMass = 2;

    public Vector2 MinStartPosition = new Vector2(-8.9f, 5f);
    public Vector2 MaxStartPosition = new Vector2(8.9f, 5f);

    [Header("Global Physics")]
    public Vector2 WindForce = new Vector2(0.1f, 0);
    public Vector2 GravityForce = new Vector2(0, -1f);
    public float FrictionCoefficient = 0.01f;

    [Header("Specific Physics")]
    public Liquid Liquid;

    protected Mover[] _Movers;

	// Use this for initialization
	void Start () 
	{
        _Movers = new Mover[Number];
        for (int i = 0; i < Number; i++)
        {
            GameObject moverObject = GameObject.Instantiate(MoverPrefab);
            Mover mover = moverObject.GetComponent<Mover>();

            Vector2 startPos = new Vector2(Random.Range(MinStartPosition.x, MaxStartPosition.x), Random.Range(MinStartPosition.y, MaxStartPosition.y));
            mover.Initialize(Random.Range(MinimumMass, MaximumMass), startPos);

            _Movers[i] = mover;
        }
	}
	
	// Update is called once per frame
	void Update () 
	{
        for (int i = 0; i < Number; i++)
        {
            Mover mover = _Movers[i];

            // Friction is velocity with 1 unit on opposite direction times friction coefficient
            Vector2 friction = mover.Velocity;
            friction *= -1;
            friction.Normalize();
            friction *= FrictionCoefficient;
            mover.ApplyForce(friction);

            // Drag force if we're in the liquid
            if (Liquid != null && Liquid.gameObject.activeSelf)
            {
                if (mover.IsOnLocation(
                    new Vector2(Liquid.Rectangle.x, Liquid.Rectangle.y - Liquid.Rectangle.height), 
                    new Vector2(Liquid.Rectangle.width, Liquid.Rectangle.height)))
                {
                    mover.ApplyDragForce(Liquid.DragCoefficient);
                }
            }

            // Wind is just wind
            mover.ApplyForce(WindForce);

            // Gravity is scaled by mass
            Vector2 gravity = new Vector2(0, mover.Mass * GravityForce.y);
            mover.ApplyForce(gravity);

            mover.UpdatePhysics();
            mover.CheckEdges();
            mover.Draw();
        }
	}
}
