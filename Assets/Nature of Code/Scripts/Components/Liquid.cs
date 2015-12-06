using UnityEngine;
using System.Collections;

public class Liquid : MonoBehaviour 
{
    public Rect Rectangle;
    public float DragCoefficient = 0.1f;

#if UNITY_EDITOR

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector2 bottomLeft = new Vector2(Rectangle.x, Rectangle.y - Rectangle.height);
        Vector2 bottomRight = new Vector2(Rectangle.x + Rectangle.width, Rectangle.y - Rectangle.height);
        Vector2 topLeft = new Vector2(Rectangle.x, Rectangle.y);
        Vector2 topRight = new Vector2(Rectangle.x + Rectangle.width, Rectangle.y);

        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);
    }

#endif
}
