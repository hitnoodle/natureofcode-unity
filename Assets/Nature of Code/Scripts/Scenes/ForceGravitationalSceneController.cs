using UnityEngine;
using System.Collections;

public class ForceGravitationalSceneController : MonoBehaviour 
{
    [Header("Object Preparations")]
    public Mover Mover;
    public Vector2 InitialForce;

    [Header("Specific Physics")]
    public Attractor Attractor;

    // Use this for initialization
    void Start () 
	{
        Mover.ApplyForce(InitialForce);
    }
	
	// Update is called once per frame
	void Update () 
	{
        Attractor.Draw();

        Vector2 attractionForce = Attractor.CalculateAttractionalForce(Mover.Mass, Mover.Location);
        Mover.ApplyForce(attractionForce);

        Mover.UpdatePhysics();
        Mover.CheckEdges();
        Mover.Draw();
    }
}
