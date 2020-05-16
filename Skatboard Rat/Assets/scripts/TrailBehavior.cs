using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailBehavior : MonoBehaviour
{
    private ParticleSystem.ShapeModule shape;
    private Transform tr;
    public Transform rat;
    private Vector2 startPos;
    
    void Start()
    {
        shape = GetComponent<ParticleSystem>().shape;
        tr = GetComponent<Transform>();
        rat = GameObject.Find("Rat(Clone)").GetComponent<Transform>();
        setReferencePoint();
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Foreground";
    }

    private void FixedUpdate()
    {
        if (rat != null)
        {
            Vector2 ratPos = rat.position;
            shape.position = new Vector2(ratPos.x + 18, ratPos.y + 2);
        }
    }

    public void setReferencePoint()
    {
        startPos = new Vector2(rat.position.x, rat.position.y);
    }
}
