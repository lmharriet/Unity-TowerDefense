using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : Singleton<TowerManager>
{
    protected TowerManager() { }
    public GameObject team;             //Tower들을 전체적으로 담고 있는 GameObject 
    public List<GameObject> allTowers = new List<GameObject>(); //전체 Tower GameObject
    public List<Building> allTowerData = new List<Building>();


    public Dictionary<EnumSpace.TEAMCOLOR, int> teamTowerCount = new Dictionary<EnumSpace.TEAMCOLOR, int>(); //각 팀이 보유한 타워
    public Dictionary<EnumSpace.TEAMCOLOR, bool> aliveTeam = new Dictionary<EnumSpace.TEAMCOLOR, bool>(); //살아 있는 팀
    public int maxTower;                //전체 타워 수
    public List<Building> departTowers = new List<Building>(); // multiselect 기능을 위한 리스트
    public Building departTower;
    public Building arriveTower;

    public EnumSpace.TEAMCOLOR playerColor; //플레이어팀 컬러지정
    public EnumSpace.TEAMCOLOR factoryColor; //factory 타워 지정

    public float def, atk;

    public GlobalDefine defineColor;

    public void ResetAllData()
    {
        allTowers.Clear();
        allTowerData.Clear();
        teamTowerCount.Clear();
        aliveTeam.Clear();
    }
    public void ResetBothTowers()
    {
        departTower = null;
        arriveTower = null;
    }

    public void SetDepartTower(RaycastHit hitInfo)
    {
        departTower = hitInfo.transform.GetComponent<Building>();
    }

    public void SetArriveTower(RaycastHit hitInfo)
    {
        arriveTower = hitInfo.transform.GetComponent<Building>();
    }

    public Vector3 GetDepartPos()
    {
        return departTower.transform.position;
    }

    public Vector3 GetArrivePos()
    {
        return arriveTower.transform.position;
    }


    public Color GetColor(int myColorNum)
    {
        //Debug.Log(GlobalDefine.colorDictionary.Count);
        return GlobalDefine.colorDictionary[(EnumSpace.TEAMCOLOR)myColorNum];
    }

    public Color GetColor(EnumSpace.TEAMCOLOR myColor)
    {
        //Debug.Log(GlobalDefine.colorDictionary.Count);
        return GlobalDefine.colorDictionary[myColor];
    }

    public float TowerATK
    {
        get { return atk; }
        set { atk = value; }
    }
    public float TowerDef
    {
        get { return def; }
        set { def = value; }
    }

    public void callScripts()
    {
        Debug.Log("towerManager is here");
    }
}