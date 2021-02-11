using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryTower : BuildingManager
{
    //유닛 수용은 가능하지만 생성 불가
    //공격력 방어력 향상
    /// private string kind = "Factory";
    public int myId;
    public int unit;          //수용하고 있는 unit
    public int level = 1;       //maxLevel =4
    public float def;         //레벨에 따른 방어력
    public float atk;         //레벨에 따른 공격력
    public TextMesh showUnit;

    private void Awake()
    {
        showUnit = transform.GetComponentInChildren<TextMesh>();
        isPlayerTeam = false;
      
    }
    void Start()
    {
        switch (level)
        {
            case 1:
                def = 1.25f;
                atk = 1.5f;
                break;
            case 2:
                def = 1.35f;
                atk = 1.75f;
                break;
            case 3:
                def = 1.45f;
                atk = 1.9f;
                break;
            case 4:
                def = 1.50f;
                atk = 2.0f;
                break;
        }
        unit = Random.Range(5, 16);
        showUnit.text = unit.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
