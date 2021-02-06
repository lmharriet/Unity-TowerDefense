using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public enum myTower
    {
        RED_TOWER, ORANGE_TOWER
    }

    //public int maxCount;
    public GameObject[] towerObj = new GameObject[2];
    public Transform[] place = new Transform[2];

    myTower tower;
    int towerKind;

    private void Awake()
    {


    }
    void Start()
    {
        towerKind = Random.Range(0, 2);
        tower = (myTower)towerKind;

        Instantiate(towerObj[(int)tower], place[(int)tower].position, Quaternion.identity);

    }

    void Update()
    {
    }

    public int TowerNumber

    {
        get { return towerKind; }
        set { towerKind = value; }
    }

}
