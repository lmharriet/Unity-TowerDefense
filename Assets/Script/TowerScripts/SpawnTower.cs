using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTower : MonoBehaviour
{
    public GameObject team;            //tower Team을 전체적으로 들고 있는 상위 오브젝트 
    public int howManyTeams = 2;       //default 2 teams
    public GameObject[] colorTeam = new GameObject[5];

    int maxTower;
    public int playerTeamNum;          //player팀으로 셋팅할 인덱스
    public int colorTowerCount;        //player, enemy타워 갯수

    //public Transform[] teamSpawnPos;
    //public GameObject[] TowerPrefs; //0 :house 1: defense 2:factory


    private void Awake()
    {
        howManyTeams = 2; // Random.Range(2, 5);

        colorTeam[0] = team.transform.GetChild(0).gameObject;
        maxTower = colorTeam[0].transform.childCount;
       
        for (int i = 1; i <=4; i++)
        {
            colorTeam[i] = team.transform.GetChild(i).gameObject;
        }

        for (int i = 1; i <=howManyTeams; i++)
        {
            //team 갯수에 따라 부모 오브젝트 활성화
            colorTeam[i].SetActive(true);

        }


        SetPlayerTeam();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetPlayerTeam()
    {
        List<int> teamNum = new List<int>();

        // int rndNum=

        for (int i = 0; i < maxTower / howManyTeams; i++)
        {
            

        }

        //int _enemyTeamNum;

        ////컬러Team을 갖고 있는 오브젝트 (현재 0은 RED ,1은  ORANGE)를 player 와 enemy에 세팅
        //playerTeamNum = Random.Range(0, 2);
        //playerTeamObj = team.transform.GetChild(playerTeamNum);

        //if (playerTeamNum > 0)
        //    _enemyTeamNum = 0;

        //else
        //    _enemyTeamNum = 1;

        ////
        //colorTowerCount = Random.Range(2, playerTeamObj.childCount - (playerTeamObj.childCount / 2));


        //for (int i = 0; i < colorTowerCount; i++)
        //{
        //    playerTeamObj.GetChild(i).GetComponent<BuildingManager>().isPlayerTeam = true;
        //    playerTeamObj.GetChild(i).GetComponent<BuildingManager>().teamColor =
        //        (BuildingManager.TOWERCOLOR)playerTeamNum + 1;

        //    EnemyTeamObj.GetChild(i).GetComponent<BuildingManager>().isPlayerTeam = false;
        //    EnemyTeamObj.GetChild(i).GetComponent<BuildingManager>().teamColor =
        //        (BuildingManager.TOWERCOLOR)_enemyTeamNum + 1;

        //}



    }
}
