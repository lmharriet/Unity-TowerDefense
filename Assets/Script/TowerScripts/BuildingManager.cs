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
    public Renderer render;
    public TextMesh showUnit;

    public enum TEAMCOLOR
    {
        NONE, RED, ORANGE, BLUE, GREEN
    }

    public TEAMCOLOR teamColor = TEAMCOLOR.NONE;

    public int unitCount
    {
        get { return unit; }
        set { unit = value; }
    }
    protected virtual void Awake()
    {
        showUnit = transform.GetComponentInChildren<TextMesh>();
        render = transform.GetComponent<Renderer>();
    }
    protected virtual void Start()
    {
        //if (isPlayerTeam)
        //{
        //    Debug.Log("my ID" + myId);
        //    Debug.Log("my teamColor" + teamColor);
        //}


    }

    protected virtual void Update()
    {
        //user team이 아닐 때
        if (!isPlayerTeam)
        {
            EnemyAI();

            //unit 개체수가 0이하로 떨어질 경우 공격한 팀으로 변경
            // CheckPlayerTeam();
        }
    }

    public void EnemyAI()
    {
        //

    }



    public void CheckAttack(TEAMCOLOR unitColor)
    {
        //타워에 부딪힌 유닛 색과 타워 색이 같으면
        if (unitColor == myColor)
        {
            unit++;
            if (isPlayerTeam)
                showUnit.text = "p" + unit.ToString();
            else
                showUnit.text = teamColor.ToString() + unit.ToString();

        }
        //타워에 부딪힌 유닛 색과 타워 색이 다르면
        else
        {
            unit--;
            //만약 타워가 가진 유닛의 개체수가 0보다 작아지면
            if (unit <= 0)
            {
                unit = 0;
                myColor = unitColor;    //현재 타워의 팀을 마지막으로 공격한 unit 팀으로 변경
                render.material.color = TowerData.Instance.GetColor(myColor);
                //현재 타워의 팀 변경
                transform.parent = TowerData.Instance.team.transform.GetChild((int)myColor);
                
                //만약 타워가 플레이어팀이었으면 현재는 점령당했으므로 플레이어팀이 아니고
                if (isPlayerTeam) isPlayerTeam = false;
                else
                {
                    //타워가 플레이어팀이 아니었으면 현재 컬러가 플레이어팀과 같은지 체크하고 
                    //플레이어팀과 같을 때 플레이어팀으로 변경
                    if (myColor == TowerData.Instance.playerColor) isPlayerTeam = true;
                }

            }


            if (isPlayerTeam)
                showUnit.text = "p" + unit.ToString();
            else
                showUnit.text = teamColor.ToString() + unit.ToString();
        }
    }

    public TEAMCOLOR myColor
    {
        get { return teamColor; }
        set { teamColor = value; }
    }


}
