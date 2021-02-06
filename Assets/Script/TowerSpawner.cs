using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    public enum TEAMNAME
    {
        RED,YELLOW,BLUE
    }
    
    public TEAMNAME team;
    public int playerTowerNumber;

    //팀 별 스폰 될 obj 위치
    public Transform[] towerPos = new Transform[3];
    //스폰 될 위치에서 스폰 될 타워
    public GameObject []towerObj = new GameObject[3];
    //한 배틀 당 설치 가능한 타워 갯수
    public int maxTowerCount;


    // Start is called before the first frame update
    void Start()
    {

        SelectPlayerTower();

        //spawn 할 타워 종류, 위치
        switch (team)
        {
            case TEAMNAME.RED:
                break;
            case TEAMNAME.YELLOW:
                break;
            case TEAMNAME.BLUE:
                
                break;
        }

    }

    public void SelectPlayerTower()
    {
        playerTowerNumber = Random.Range(0, 3);
        team = (TEAMNAME)playerTowerNumber;
    }


}
