using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTower : MonoBehaviour
{
    public Transform[] teamSpawnPos;
    public GameObject[] TowerPrefs; //0 :house 1: defense 2:factory
    int maxTower = 8;
    int teamCount;
    bool isCreat;
    // Start is called before the first frame update
    void Start()
    {
        teamCount = Random.Range(2, 4);
    }

    // Update is called once per frame
    void Update()
    {
        //team이 2개 일 때 :
        //team 1 은 위 team 2은 아래

        //if(teamCount ==2)
        //{

        //}

        if (!isCreat)
        {
            for (int i = 0; i < 2; i++)
            {
                Instantiate(TowerPrefs[0], teamSpawnPos[0].GetChild(i).GetComponentInChildren<Transform>().position, Quaternion.identity);

                if (i == 1)
                {
                    Instantiate(TowerPrefs[1], teamSpawnPos[0].GetChild(2).GetComponentInChildren<Transform>().position,
                        Quaternion.identity);
                    Instantiate(TowerPrefs[2], teamSpawnPos[0].GetChild(3).GetComponentInChildren<Transform>().position,
                        Quaternion.identity);
                    isCreat = true;
                }
            }
        }




        //team이 3개 일 떄 :
        //team1은 왼쪽 team 2는 중간 team 3은 오른쪽
    }
}
