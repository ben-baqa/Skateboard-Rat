using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    public GameObject[] obstacles;
    public int[] delay;

    private RatBehavior rat;
    private int[] timer;

    void Start()
    {
        timer = new int[obstacles.Length];
        foreach(int i in timer)
        {
            timer[i] = 0;
        }
        float[] positions = new float[5];
        for (int i = 0; i < 5; i++)
        {
            positions[i] = Random.Range(-30 + 18 * i, -12 + 18 * i);
        }
        foreach (float x in positions)
        {
            int index = (int)Random.Range(0, obstacles.Length);
            float y = obstacles[index].GetComponent<Transform>().position.y;
            GameObject inst = Instantiate(obstacles[index], new Vector2(x, y), Quaternion.identity);
            inst.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.5f, 1), Random.Range(0.5f, 1), Random.Range(0.5f, 1));
        }
    }

    private void FixedUpdate()
    {
        if (rat == null)
        {
            rat = GameObject.Find("Rat(Clone)").GetComponent<RatBehavior>();
        }
        for(int i = 0; i < obstacles.Length; i++)
        {
            if (timer[i] > delay[i] / (0.0001 + Mathf.Pow(rat.speed, 4)))
            {
                if ((int)Random.Range(0, 100) == 8)
                {
                    SpawnObject(i);
                    timer[i] = 0;
                }
            }
            timer[i]++;
        }
    }

    private void SpawnObject(int n)
    {
        GameObject inst = Instantiate(obstacles[n]);
        inst.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.5f, 1), Random.Range(0.5f, 1), Random.Range(0.5f, 1));
    }
}
