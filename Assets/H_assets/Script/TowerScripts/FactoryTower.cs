﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryTower : Building
{
    public FactoryTower(int Id, int units, int myLevel, bool isPlayer, EnumSpace.TEAMCOLOR col) { }

    //유닛 수용은 가능하지만 생성 불가
    //공격력 방어력 향상
    /// private string kind = "Factory";
    public float def;         //레벨에 따른 방어력
    public float atk;         //레벨에 따른 공격력

    protected override void Awake()
    {
        base.Awake();
        maxLevel = 4;
        kind = EnumSpace.TOWERKIND.FACTORY;
        //showUnit = transform.GetComponentInChildren<TextMesh>();

    }
    protected override void Start()
    {
        base.Start();

    }

    protected override void SetStatByLevel()
    {
        switch (level)
        {
            case 1:
                def = 1.25f;
                atk = 1.5f;
                //0.25
                upgradeCost = 5;
                break;
            case 2:
                def = 1.35f;
                atk = 1.75f;
                //0.4
                upgradeCost = 10;
                break;
            case 3:
                def = 1.45f;
                atk = 1.9f;
                //0.45
                upgradeCost = 15;
                break;
            case 4:
                def = 1.50f;
                atk = 2.0f;
                break;
                //0.5
        }
        TowerManager.Instance.TowerATK = atk;
        TowerManager.Instance.TowerDef = def;
        unit = Random.Range(5, 16);

        SetTextMesh();

    }

}