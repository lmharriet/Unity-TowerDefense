using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    protected int unitNum;
    protected int maxUnit = 100;
    protected float delay;
    protected float spawnTime = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
    }

    protected void SpawnUnit()
    {
        if (maxUnit < unitNum) return;

        if (spawnTime > delay)
        {
            spawnTime = 0f;
            unitNum += 1;
        }
    }
}
