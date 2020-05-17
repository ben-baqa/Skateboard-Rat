using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameObject rat, oSpawner, trail;
    public bool reset;

    private GameObject ratInst, spawnInst, trailInst;
    private Text bestScoreDisplay;
    private int bestScore;
    
    void Start()
    {
        bestScoreDisplay = GameObject.Find("Best Score").GetComponent<Text>();
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
        //ratInst = Instantiate(rat, new Vector2(-16, 20), Quaternion.identity);
        ratInst = Instantiate(rat);
        spawnInst = Instantiate(oSpawner);
        trailInst = Instantiate(trail);//.GetComponent<TrailBehavior>().rat = ratInst.transform;
    }

    public void OnDeath()
    {
        int num = ratInst.GetComponent<RatBehavior>().score;
        if (num > bestScore)
        {
            bestScore = num;
            bestScoreDisplay.text = num.ToString();
        }
        Destroy(spawnInst);
        Destroy(trailInst);
        GameObject[] tobeDestroyed = GameObject.FindGameObjectsWithTag("destroyOnDeath");
        foreach (GameObject item in tobeDestroyed)
        {
            item.gameObject.GetComponent<ObstacleBehavior>().OnDeath();
        }
    }
}
