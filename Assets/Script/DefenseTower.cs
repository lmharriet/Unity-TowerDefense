using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseTower : Building
{
    //공격하는 타워
    
    public GameObject bombPref;
    public Transform firePos;
    
    //타겟이 범위에 들어오면 유닛 공격(우리팀이 아닐 경우)
    public float range;
    // Start is called before the first frame update
    void Start()
    {
        range = 5f;
        delay = 3f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        spawnTime += Time.deltaTime;
        SpawnUnit();
    }
}
