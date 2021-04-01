using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject team;            //tower Team을 전체적으로 들고 있는 상위 오브젝트 
    public int howManyTeams;
    public GameObject[] colorOfTeam = new GameObject[5];

    int maxTower;
    public int playerTeamNum;          //player팀으로 셋팅할 인덱스
    public int colorTowerCount;        //player, enemy타워 갯수


    private void Start()
    {

        maxTower = team.transform.childCount;
        TowerManager.Instance.maxTower = maxTower;

        for (int i = 0; i < maxTower; i++)
        {
            TowerManager.Instance.allTowers.Add(team.transform.GetChild(i).gameObject);
            TowerManager.Instance.allTowerData.Add(team.transform.GetChild(i).transform.GetComponent<Building>());
            if (team.transform.GetChild(i).GetComponent<Building>().isPlayerTeam)
            {
                TowerManager.Instance.playerColor = team.transform.GetChild(i).transform.GetComponent<Building>().myColor;
            }

            //팀별 타워 소유 갯수 저장
            if (TowerManager.Instance.teamTowerCount.ContainsKey(TowerManager.Instance.allTowerData[i].myColor) == false)
            {
                TowerManager.Instance.teamTowerCount.Add(TowerManager.Instance.allTowerData[i].myColor, 1);
                TowerManager.Instance.arriveTeam.Add(TowerManager.Instance.allTowerData[i].myColor, 1);
            }
            else
            {
                TowerManager.Instance.teamTowerCount[TowerManager.Instance.allTowerData[i].myColor]++;
            }

        }


    }

}
