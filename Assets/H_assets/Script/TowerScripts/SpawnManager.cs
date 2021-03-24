using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public bool isFreeMode;
    public GameObject team;            //tower Team을 전체적으로 들고 있는 상위 오브젝트 
    public int howManyTeams;
    public GameObject[] colorOfTeam = new GameObject[5];


    int maxTower;
    public int playerTeamNum;          //player팀으로 셋팅할 인덱스
    public int colorTowerCount;        //player, enemy타워 갯수


    private void Awake()
    {

        if (isFreeMode)
        {
            howManyTeams = Random.Range(2, 5);
            TowerManager.Instance.team = team;
            //color가 None인 obj
            colorOfTeam[0] = team.transform.GetChild(0).gameObject;
            maxTower = colorOfTeam[0].transform.childCount;
            TowerManager.Instance.maxTower = maxTower;
            for (int i = 1; i <= 4; i++)
            {
                colorOfTeam[i] = team.transform.GetChild(i).gameObject;
            }

            for (int i = 1; i <= howManyTeams; i++)
            {
                //team 갯수에 따라 부모 오브젝트 활성화
                colorOfTeam[i].SetActive(true);
            }

            DivideTeam();
        }
        else
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

    public void DivideTeam()
    {
        //List<GameObject> divisionTeam = new List<GameObject>();

        //전체 타워 갯수를 팀의 갯수만큼 나눈 몫을 division에 저장
        //만약 타워가 12이고 팀이 2면 ->6
        int division = maxTower / howManyTeams;
        GameObject _team;
        for (int i = 0; i < maxTower; i++)
        {
            _team = colorOfTeam[0].transform.GetChild(i).gameObject;
            //colorOfTeam[0] = "NONE"obj
            TowerManager.Instance.allTowers.Add(_team);
            TowerManager.Instance.allTowerData.Add(_team.GetComponent<Building>());
            //divisionTeam.Add(colorOfTeam[0].transform.GetChild(i).gameObject);
        }


        // 0 1 2 3 4 5 
        // 6 7 8 9 10 11
        //어떤 숫자 중 -1, +1을 했을 때, 둘 다 값이 존재하는 범위 지정 후 그 범위에서 랜덤  ex)1 ~ 4
        int selectTowers = Random.Range(1, division - 1);

        for (int i = 0; i < howManyTeams; i++)
        {
            int _firstIndex = selectTowers + (i * division);
            int _secondIndex = (selectTowers - 1) + (i * division);

            Transform _firstTransform = TowerManager.Instance.allTowers[_firstIndex].transform;
            Transform _secondTransform = TowerManager.Instance.allTowers[_secondIndex].transform;


            //팀의 갯수만큼 균등하게 NOME의 차일드 타워를 분배 , color 색 지정
            _firstTransform.parent = colorOfTeam[i + 1].transform;
            _firstTransform.GetComponent<Building>().myTeam = (EnumSpace.TEAMCOLOR)i + 1;

            _firstTransform.GetComponent<Renderer>().material.color = TowerManager.Instance.GetColor(i + 1);


            _secondTransform.parent = colorOfTeam[i + 1].transform;
            _secondTransform.GetComponent<Building>().myTeam = (EnumSpace.TEAMCOLOR)i + 1;

            _secondTransform.GetComponent<Renderer>().material.color = TowerManager.Instance.GetColor(i + 1);

        }


        int _playerTeam = Random.Range(1, howManyTeams + 1); //1 2 3 4

        TowerManager.Instance.playerColor = (EnumSpace.TEAMCOLOR)_playerTeam;

        int _child = colorOfTeam[_playerTeam].transform.childCount;

        for (int i = 0; i < _child; i++)
        {
            colorOfTeam[_playerTeam].transform.GetChild(i).
                GetComponent<Building>().isPlayerTeam = true;

        }

    }

}
