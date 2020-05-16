using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] Obstacles;

    public float delay;
    public int initialDelay;

    private float timer;

    void Start()
    {
        timer = -initialDelay;
    }

    private void FixedUpdate()
    {
        if(timer > delay)
        {
            if ((int)Random.Range(0, 20) == 8)
            {
                SpawnObject(0);
                timer = 0;
            }
        }
        timer++;
    }

    private void SpawnObject(int n)
    {
        Instantiate(Obstacles[0]);
    }
}
