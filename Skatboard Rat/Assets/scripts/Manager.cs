using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject rat, oSpawner, trail;
    public bool reset;

    private GameObject ratInst, spawnInst, trailInst;
    
    void Start()
    {
        reset = false;
        Reset();
    }

    void Update()
    {
        if (reset)
        {
            Reset();
            reset = false;
        }
    }

    public void Reset()
    {
        Destroy(ratInst);
        ratInst = Instantiate(rat, new Vector2(-16, 20), Quaternion.identity);
        spawnInst = Instantiate(oSpawner);
        trailInst = Instantiate(trail);//.GetComponent<TrailBehavior>().rat = ratInst.transform;
    }

    public void OnDeath()
    {

        Destroy(spawnInst);
        Destroy(trailInst);
    }
}
