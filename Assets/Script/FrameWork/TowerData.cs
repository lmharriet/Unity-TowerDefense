using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerData : Singleton<TowerData>
{
    protected TowerData() { }

    int id;
    

    public int TowardTowerID
    {
        get { return id; }
        set { id = value; }
    }
    

}
