using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DefenseTower : Building
{
    //공격하는 타워 (default- 팀에 속하지 않음)
    
    public GameObject bombPref;
    public Transform firePos;
    public GameObject target;
    
    //타겟이 범위에 들어오면 유닛 공격(우리팀이 아닐 경우)
    public float range;
    //팀에 속하지 않을 때, 가장 먼저  hp만큼의 유닛을 보낸 팀이 공격하는 타워를 선점가능
    public int hp;


 
    protected override void Awake()
    {
        base.Awake();
        hp = 5;
        range = 5f;
        InitSpawnUnit(2f);
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        if(hp<=0)
        {
            hp = 0;
            spawnTime += Time.deltaTime;
            SpawnUnit();
        }
    }

    public void attack()
    {

    }
}
