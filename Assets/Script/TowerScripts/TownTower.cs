using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownTower : BuildingManager
{

    //마을 레벨이 높을 수록 전사가 더 빨리 생산됨
    //max level = 5
    /// private string kind = "Town";

    public int maxCapacity;  // 20 40 60 80 100 수용 가능
    public float time = 0f;


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
                maxCapacity = 20;
                unit = Random.Range(5, maxCapacity - 6);
                break;
            case 2:
                maxCapacity = 40;
                unit = Random.Range(10, maxCapacity - 16);
                break;
            case 3:
                maxCapacity = 60;
                unit = Random.Range(15, maxCapacity - 26);
                break;
            case 4:
                maxCapacity = 80;
                unit = Random.Range(20, maxCapacity - 36);
                break;
            case 5:
                maxCapacity = 100;
                unit = Random.Range(40, maxCapacity - 46);
                break;
        }

        if (isPlayerTeam)
            showUnit.text = unit.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > 2f)
        {
            time = 0f;
            unit++;
            if (isPlayerTeam)
                showUnit.text = unit.ToString();
        }

    }
}
