using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseTower : Building
{
    protected override void Awake()
    {
        base.Awake();
        level = 1;

        if (level == 1)
            InitSpawnUnit(3f, 5);
        else
            InitSpawnUnit(2f, 10);
    }

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        spawnTime += Time.deltaTime;
        SpawnUnit();
        //StartCoroutine(DragObject());
       
    }


}
