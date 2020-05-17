using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleClear : MonoBehaviour
{
    public GameObject burst;
    private Transform rat;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        rat = GameObject.Find("Rat(Clone)").GetComponent<Transform>();
        if (rat != null)
        {
            Instantiate(burst, rat.position, Quaternion.identity);
        }
    }
}
