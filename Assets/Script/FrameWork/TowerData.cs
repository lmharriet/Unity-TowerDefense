using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerData : Singleton<TowerData>
{
    protected TowerData() { }
    public GameObject team;
    public List<GameObject> allTowers = new List<GameObject>();
    public List<BuildingManager> allTowerData = new List<BuildingManager>();
   
    public BuildingManager departTower;
    public BuildingManager arriveTower;
    public TEAMCOLOR playerColor;

    public int maxTower;

    public Color color;
    public Dictionary<TEAMCOLOR, Color> haveColor = new Dictionary<TEAMCOLOR, Color>();
  
    
    public void Awake()
    {
        color = Color.white;
        haveColor.Add(TEAMCOLOR.NONE, color);
        color = Color.red;
        haveColor.Add(TEAMCOLOR.RED, color);
        color = Color.yellow;
        haveColor.Add(TEAMCOLOR.ORANGE, color);
        color = Color.blue;
        haveColor.Add(TEAMCOLOR.BLUE, color);
        color = Color.green;
        haveColor.Add(TEAMCOLOR.GREEN, color);
    }
    public void ResetBothTowers()
    {
        departTower = null;
        arriveTower = null;
    }

    public void SetDepartTower(RaycastHit hitInfo)
    {
        departTower = hitInfo.transform.GetComponent<BuildingManager>();
    }

    public void SetArriveTower(RaycastHit hitInfo)
    {
        arriveTower = hitInfo.transform.GetComponent<BuildingManager>();
    }

    public Vector3 GetDepartPos()
    {
        return departTower.transform.position;
    }

    public Vector3 GetArrivePos()
    {
        return arriveTower.transform.position;
    }

    public Color GetColor(int teamNum)
    {

        return haveColor[(TEAMCOLOR)teamNum];
    }
    public Color GetColor(TEAMCOLOR teamColor)
    {

        return haveColor[teamColor];
    }

}