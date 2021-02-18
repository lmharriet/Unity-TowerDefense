using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerData : Singleton<TowerData>
{
    protected TowerData() { }
    public GameObject team;
    public List<GameObject> allTowers = new List<GameObject>();
    public List<Building> allTowerData = new List<Building>();
   
    public Building departTower;
    public Building arriveTower;
    public EnumSpace.TEAMCOLOR playerColor;

    public int maxTower;

    public Color color;
    public Dictionary<EnumSpace.TEAMCOLOR, Color> colorData = new Dictionary<EnumSpace.TEAMCOLOR, Color>();
  
    
    public void Awake()
    {
        color = Color.white;
        colorData.Add(EnumSpace.TEAMCOLOR.NONE, color);
        color = Color.red;
        colorData.Add(EnumSpace.TEAMCOLOR.RED, color);
        color = Color.yellow;
        colorData.Add(EnumSpace.TEAMCOLOR.YELLOW, color);
        color = Color.blue;
        colorData.Add(EnumSpace.TEAMCOLOR.BLUE, color);
        color = Color.green;
        colorData.Add(EnumSpace.TEAMCOLOR.GREEN, color);
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

    public Color GetColor(int teamNum)
    {

        return colorData[(EnumSpace.TEAMCOLOR)teamNum];
    }
    public Color GetColor(EnumSpace.TEAMCOLOR teamColor)
    {

        return colorData[teamColor];
    }

}