using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject team;            //tower Team을 전체적으로 들고 있는 상위 오브젝트 
    public int howManyTeams = 2;       //default 2 teams
    public GameObject[] colorOfTeam = new GameObject[5];

    int maxTower;
    public int playerTeamNum;          //player팀으로 셋팅할 인덱스
    public int colorTowerCount;        //player, enemy타워 갯수

    //public Transform[] teamSpawnPos;
    //public GameObject[] TowerPrefs; //0 :house 1: defense 2:factory


    private void Awake()
    {
        howManyTeams = Random.Range(2, 5);
        TowerData.Instance.team = team;
        //color가 None인 obj
        colorOfTeam[0] = team.transform.GetChild(0).gameObject;
        maxTower = colorOfTeam[0].transform.childCount;
        TowerData.Instance.maxTower = maxTower;
        for (int i = 1; i <= 4; i++)
        {
            colorOfTeam[i] = team.transform.GetChild(i).gameObject;
        }

        for (int i = 1; i <= howManyTeams; i++)
        {
            //team 갯수에 따라 부모 오브젝트 활성화
            colorOfTeam[i].SetActive(true);
        }

        //
        DivideTeam();

    }

    // Update is called once per frame
    void Update()
    {

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
            TowerData.Instance.allTowers.Add(_team);
            TowerData.Instance.allTowerData.Add(_team.GetComponent<Building>());
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

            Transform _firstTransform = TowerData.Instance.allTowers[_firstIndex].transform;
            Transform _secondTransform = TowerData.Instance.allTowers[_secondIndex].transform;


            //팀의 갯수만큼 균등하게 NOME의 차일드 타워를 분배 , color 색 지정
            _firstTransform.parent = colorOfTeam[i + 1].transform;
            _firstTransform.GetComponent<Building>().teamColor = (EnumSpace.TEAMCOLOR)i + 1;

            _firstTransform.GetComponent<Renderer>().material.color = TowerData.Instance.GetColor(i+1);


            _secondTransform.parent = colorOfTeam[i + 1].transform;
            _secondTransform.GetComponent<Building>().teamColor = (EnumSpace.TEAMCOLOR)i + 1;

            _secondTransform.GetComponent<Renderer>().material.color = TowerData.Instance.GetColor(i+1);

        }


        int _playerTeam = Random.Range(1, howManyTeams + 1); //1 2 3 4

        TowerData.Instance.playerColor = (TEAMCOLOR)_playerTeam;

        int _child = colorOfTeam[_playerTeam].transform.childCount;

        for (int i = 0; i < _child; i++)
        {
            colorOfTeam[_playerTeam].transform.GetChild(i).
                GetComponent<Building>().isPlayerTeam = true;

        }

    }
}
