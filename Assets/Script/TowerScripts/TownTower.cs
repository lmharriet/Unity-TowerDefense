using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownTower : MonoBehaviour
{

    //마을 레벨이 높을 수록 전사가 더 빨리 생산됨
    //max level = 5


    /// private string kind = "Town";
    public int myId;         // 몇 번째 타워인지
    public int level=1;        //max level = 5
    public int unit = 0;
    public int maxCapacity;  // 20 40 60 80 100 수용 가능
    public int upgradeCost;     //     5 10 20     비용으로 업그레이드 가능
    public float time = 0f;

    public TextMesh showUnit;

    private void Awake()
    {
        showUnit = transform.GetComponentInChildren<TextMesh>();
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
            showUnit.text = unit.ToString();
        }

    }
}
