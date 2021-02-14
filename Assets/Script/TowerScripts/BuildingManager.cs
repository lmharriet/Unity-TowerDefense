using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public bool isPlayerTeam;
    public string kind;
    public int myId;
    public int unit;
    public int level = 1;
    public int upgradeCost;
    public TextMesh showUnit;

    public int unitCount
    {
        get { return unit; }
        set { unit = value; }
    }

    protected virtual void Update()
    {
        if(!isPlayerTeam)
        {
            if(unit<=0)
            {
                unit = 0;
                isPlayerTeam = true;
                showUnit.text = "P" + unit.ToString();
            }
        }
    }
    
}
