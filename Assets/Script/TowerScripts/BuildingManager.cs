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
        
        if (isPlayerTeam)
        {
            Debug.Log("my ID" + myId);
            Debug.Log("my teamColor" + teamColor);
        }


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

        if (unitColor == myColor)
        {
            unit++;
            if (isPlayerTeam)
                showUnit.text = "p" + unit.ToString();
            else
                showUnit.text = teamColor.ToString() + unit.ToString();

        }

        else
        {
            unit--;
            if (unit <= 0)
            {
                unit = 0;
                myColor = unitColor;
                render.material.color = TowerData.Instance.GetColor(myColor);
                transform.parent = TowerData.Instance.team.transform.GetChild((int)myColor);
                
                if (isPlayerTeam) isPlayerTeam = false;
                else
                {
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
