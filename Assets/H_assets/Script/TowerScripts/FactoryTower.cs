using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryTower : Building
{
    //유닛 수용은 가능하지만 생성 불가
    //공격력 방어력 향상
    /// private string kind = "Factory";
    public float def;         //레벨에 따른 방어력
    public float atk;         //레벨에 따른 공격력

    protected override void Awake()
    {
        base.Awake();
        level = 1;
        kind = EnumSpace.TOWERKIND.FACTORY;
        //showUnit = transform.GetComponentInChildren<TextMesh>();

    }
    protected override void Start()
    {
        base.Start();


        //if (isPlayerTeam)
        //    showUnit.text = "P"+unit.ToString();
        //else
        //    showUnit.text = myTeam + unit.ToString();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void SetStatByLevel()
    {
        switch (level)
        {
            case 1:
                def = 1.25f;
                atk = 1.5f;
                upgradeCost = 5;
                break;
            case 2:
                def = 1.35f;
                atk = 1.75f;
                upgradeCost = 10;
                break;
            case 3:
                def = 1.45f;
                atk = 1.9f;
                upgradeCost = 15;
                break;
            case 4:
                def = 1.50f;
                atk = 2.0f;
                break;
        }
        unit = Random.Range(5, 16);
    }

}