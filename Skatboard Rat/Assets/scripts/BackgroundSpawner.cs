using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    public GameObject[] obstacles;
    public float delay;
    public int obstaclesLength;

    private RatBehavior rat;
    private float timer;

    void Start()
    {
        //rat = GameObject.Find("Rat(Clone)").GetComponent<RatBehavior>();
        timer = 0;
    }

    private void FixedUpdate()
    {
        if (rat == null)
        {
            rat = GameObject.Find("Rat(Clone)").GetComponent<RatBehavior>();
        }
        if (timer > delay / (0.0001 + Mathf.Pow(rat.speed, 4)))
        {
            if ((int)Random.Range(0, 100) == 8)
            {
                SpawnObject((int)Random.Range(0, obstacles.Length));
                timer = 0;
            }
        }
        timer++;
    }

    private void SpawnObject(int n)
    {
        GameObject inst = Instantiate(obstacles[n]);
        inst.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.5f, 1), Random.Range(0.5f, 1), Random.Range(0.5f, 1));
    }
}
