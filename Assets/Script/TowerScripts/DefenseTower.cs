using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseTower : BuildingManager
{
    //유효 사정거리에 있는 적을 사격
    //방어력을 높여주지만, 유닛을 생산하지는 않음
    ///private string kind = "Defense";
    public int myId;
    public int level = 1;               //maxLevel =4
    public int unit;                    //현재 수용하고 있는 유닛
    public float range;                 //레벨에 따른 공격 범위
    public float def;                   //레벨에 따른 방어력
    private int upgradeCost=20;
    //public bool isPlayerTeam;
    public TextMesh showUnit;

    private void Awake()
    {
        showUnit = transform.GetComponentInChildren<TextMesh>();
        isPlayerTeam = false;
        
    }
    void Start()
    {
        switch(level)
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
        showUnit.text = unit.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
