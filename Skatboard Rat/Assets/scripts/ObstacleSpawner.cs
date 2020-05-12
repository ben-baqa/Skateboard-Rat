using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] Obstacles;

    public float delay;

    private float timer;

    void Start()
    {
        timer = 0;
    }

    private void FixedUpdate()
    {
        if(timer > delay)
        {
            SpawnObject(0);
            timer = 0;
        }
        timer++;
    }

    private void SpawnObject(int n)
    {
        Instantiate(Obstacles[0]);
    }
}
