using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public bool isPlayerTeam;
    public string kind;
    public int myId;
    public int unit;
    public int level = 1;
    public int upgradeCost;
    public TextMesh showUnit;


    public enum TOWERCOLOR
    {
        NONE,RED,ORANGE
    }

    public TOWERCOLOR teamColor = TOWERCOLOR.NONE;

    public int unitCount
    {
        get { return unit; }
        set { unit = value; }
    }
    protected virtual void Start()
    {
        if(isPlayerTeam)
        {
            Debug.Log("my ID" +myId);
            Debug.Log("my teamColor" +teamColor);
        }
    }

    protected virtual void Update()
    {
        //user team이 아닐 때
        if (!isPlayerTeam)
        {
            EnemyAI();

            //unit 개체수가 0이하로 떨어질 경우 공격한 팀으로 변경
            CheckPlayerTeam();
        }
    }

    public void EnemyAI()
    {
        //
       
    }



    public void CheckPlayerTeam()
    {
        if (unit <= 0)
        {
            unit = 0;
            isPlayerTeam = true;
            showUnit.text = "P" + unit.ToString();
        }
    }

    
}
