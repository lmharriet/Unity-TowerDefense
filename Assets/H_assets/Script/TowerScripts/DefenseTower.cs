﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class DefenseTower : Building
{
    public DefenseTower(int Id, int units, int myLevel, bool isPlayer, EnumSpace.TEAMCOLOR col) { }
    //유효 사정거리에 있는 적을 사격
    //방어력을 높여주지만, 유닛을 생산하지는 않음
    ///private string kind = "Defense";

    private float range;                 //레벨에 따른 공격 범위
    public float def;                   //레벨에 따른 방어력
    public bool doNotice;
    public TurretAttack turret;
    protected override void Awake()
    {
        base.Awake();
        upgradeCost = 20;
        maxLevel = 4;
        kind = EnumSpace.TOWERKIND.DEFENSE;
    }
    protected override void Start()
    {
        base.Start();

        upgradeCost = 20;
    }

    protected override void SetStatByLevel()
    {
        switch (level)
        {
            case 1:
                range = 1.0f;
                def = 1.4f;
                break;
            case 2:
                range = 1.1f;
                def = 1.7f;
                break;
            case 3:
                range = 1.25f;
                def = 1.9f;
                break;
            case 4:
                range = 1.4f;
                def = 2.0f;
                break;
        }
        unit = Random.Range(10, 20);

        SetTextMesh();

    }

 
    public float GetRange()
    {
        return range;
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.up, 10f);
    }


}

