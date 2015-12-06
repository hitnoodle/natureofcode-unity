using UnityEngine;
using System.Collections;

public class ForceAttractionalSceneController : MonoBehaviour 
{
    [Header("Object Preparations")]
    public GameObject MoverPrefab;
    public int Number = 10;

    public float MinimumMass = 0.1f;
    public float MaximumMass = 2;

    public Vector2 MinStartPosition = new Vector2(-8.9f, 5f);
    public Vector2 MaxStartPosition = new Vector2(8.9f, 5f);

    [Header("Specific Physics")]
    public Attractor Attractor;

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
    void Update()
    {
        if (Attractor != null && Attractor.gameObject.activeSelf)
            Attractor.Draw();

        for (int i = 0; i < Number; i++)
        {
            Mover mover = _Movers[i];

            if (Attractor != null && Attractor.gameObject.activeSelf)
            {
                Vector2 attractionForce = Attractor.CalculateAttractionalForce(mover.Mass, mover.Location);
                mover.ApplyForce(attractionForce);
            }

            for (int j = 0; j < Number; j++)
            {
                if (i != j)
                {
                    Mover otherMover = _Movers[j];
                    Vector2 attractionForce = otherMover.CalculateAttractionalForce(mover.Mass, mover.Location);
                    mover.ApplyForce(attractionForce);
                }
            }

            mover.UpdatePhysics();
            mover.CheckEdges();
            mover.Draw();
        }
    }
}
