using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleClear : MonoBehaviour
{
    public GameObject burst;
    private Transform rat;

    void Start()
    {
        rat = GameObject.Find("Rat(Clone)").GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (rat != null)
        {
            Instantiate(burst, rat.position, Quaternion.identity);
        }
    }
}
