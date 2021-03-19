using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TownTower : Building
{

    TownTower(int Id, int units, int myLevel, bool isPlayer, EnumSpace.TEAMCOLOR col) { }
    //마을 레벨이 높을 수록 전사가 더 빨리 생산됨
    //max level = 5
    /// private string kind = "Town";



    public float time = 0f;
    public float spawnDelay;

    protected override void Awake()
    {
        base.Awake();
        maxLevel = 5;
        kind = EnumSpace.TOWERKIND.TOWN;
        //showUnit = transform.GetComponentInChildren<TextMesh>();
    }


    protected override void Start()
    {
        base.Start();

    }

    protected override void Update()
    {
        base.Update();


        if (myTeam != EnumSpace.TEAMCOLOR.NONE
            && unit < maxCapacity)
        {
            time += Time.deltaTime;

            if (time > spawnDelay)
            {
                time = 0f;
                unit++;
                if (isPlayerTeam)
                    showUnit.text = "P" + unit.ToString();
            }
        }
        else if (myTeam == EnumSpace.TEAMCOLOR.NONE)
        {
            showUnit.text = unit.ToString();
        }

    }


    protected override void SetStatByLevel()
    {
        switch (level)
        {
            case 1:
                maxCapacity = 20;//capacity[level];
                upgradeCost = 5;
                spawnDelay = 2f;
                if (!isSetStartStat)
                    unit = Random.Range(5, maxCapacity - 6);
                break;
            case 2:
                maxCapacity = 40;
                upgradeCost = 10;
                spawnDelay = 2f;
                if (!isSetStartStat)
                    unit = Random.Range(10, maxCapacity - 16);
                break;
            case 3:
                maxCapacity = 60;
                upgradeCost = 15;
                spawnDelay = 1.7f;
                if (!isSetStartStat)
                    unit = Random.Range(15, maxCapacity - 26);
                break;
            case 4:
                maxCapacity = 80;
                upgradeCost = 20;
                spawnDelay = 1.5f;
                if (!isSetStartStat)
                    unit = Random.Range(20, maxCapacity - 36);
                break;
            case 5:
                maxCapacity = 100;
                spawnDelay = 1.3f;
                if (!isSetStartStat)
                    unit = Random.Range(40, maxCapacity - 46);
                break;
        }

        SetTextMesh();
    }
}
