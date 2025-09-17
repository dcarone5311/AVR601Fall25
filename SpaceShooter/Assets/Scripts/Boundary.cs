using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class Boundary : MonoBehaviour
{
    static public Boundary instance;

    public float boundaryWidth, boundaryLength;

    Vector2 topRight, topLeft, bottomRight, bottomLeft;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        topRight = new Vector2(boundaryLength / 2f, boundaryWidth / 2f);
        topLeft = new Vector2(-boundaryLength / 2f, boundaryWidth / 2f);
        bottomRight = new Vector2(boundaryLength / 2f, -boundaryWidth / 2f);
        bottomLeft = new Vector2(-boundaryLength / 2f, -boundaryWidth / 2f);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topLeft, bottomLeft);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomLeft, bottomRight);
    }
}
