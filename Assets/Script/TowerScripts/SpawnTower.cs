using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTower : MonoBehaviour
{
    public Transform[] teamSpawnPos;
    int maxTower=8;
    int teamCount;
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
        
        



        //team이 3개 일 떄 :
        //team1은 왼쪽 team 2는 중간 team 3은 오른쪽
    }
}
