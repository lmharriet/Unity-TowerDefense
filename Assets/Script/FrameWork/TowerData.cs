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
    public BuildingManager.TEAMCOLOR playerColor;

    public int maxTower;

    public Color color;
    public Dictionary<BuildingManager.TEAMCOLOR, Color> haveColor = new Dictionary<BuildingManager.TEAMCOLOR, Color>();
  
    
    public void Awake()
    {
        color = Color.white;
        haveColor.Add(BuildingManager.TEAMCOLOR.NONE, color);
        color = Color.red;
        haveColor.Add(BuildingManager.TEAMCOLOR.RED, color);
        color = Color.yellow;
        haveColor.Add(BuildingManager.TEAMCOLOR.ORANGE, color);
        color = Color.blue;
        haveColor.Add(BuildingManager.TEAMCOLOR.BLUE, color);
        color = Color.green;
        haveColor.Add(BuildingManager.TEAMCOLOR.GREEN, color);
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

        return haveColor[(BuildingManager.TEAMCOLOR)teamNum];
    }
    public Color GetColor(BuildingManager.TEAMCOLOR teamColor)
    {

        return haveColor[teamColor];
    }

}