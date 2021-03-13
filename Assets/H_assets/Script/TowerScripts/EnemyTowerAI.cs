using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTowerAI : MonoBehaviour
{
    public List<GameObject> towers = new List<GameObject>();
    public GameObject currentTower;
    int myIndex;


    int compare(KeyValuePair<int, float> a, KeyValuePair<int, float> b)
    {
        if (a.Value > b.Value) return 1;
        else if (a.Value < b.Value) return -1;
        else return 0;
    }
    public void SortTowersByDistance(List<GameObject> allTowers, Transform myTower)
    {
        int maxTower = allTowers.Count;

        List<KeyValuePair<int, float>> distancesPair = new List<KeyValuePair<int, float>>();

        for (int i = 0; i < maxTower; i++)
        {
            float dis = Vector3.Distance(allTowers[i].transform.position, myTower.position);

            if (dis <= 0.1f)
            {
                myIndex = i;
                currentTower = allTowers[myIndex].gameObject;
            }
            else
            {
                distancesPair.Add(new KeyValuePair<int, float>(i, dis));
            }
        }

        distancesPair.Sort(compare);

        //foreach (var it in distancesPair)
        //{
        //    //Debug.Log(it.Key);
        //    Debug.Log(it.Value);
        //}


        towers.Clear();
        for (int i = 0; i < distancesPair.Count; i++)
        {
            towers.Add(allTowers[distancesPair[i].Key]);
        }

        //1.distance between i and other towers //타워 게임오브젝트 필요
        //2.sort from near distance to far distance
        //3.add.() 

    }
    public GameObject SelectTowerToOccupy()
    {
        //1. 가장 가까운 타워가 점령 가능하면 return tower to send unit;
        int index = 0;

        while (index < towers.Count)
        {
            if (towers[index].transform.GetComponent<Building>().myTeam != EnumSpace.TEAMCOLOR.NONE)
                index++;
            else
            {
                int calculateUnit = (int)(currentTower.transform.GetComponent<Building>().unitCount * 0.75f);
                if (towers[index].transform.GetComponent<Building>().unitCount <= calculateUnit)
                {
                    return towers[index].gameObject;
                }
                else
                    index++;
            }
        }
        return null;
    }

    public GameObject SelectTowerToAttack()
    {
        //만약 내 타워의 유닛수가 가까운 다른팀이 갖고 있는 유닛수 보다 많을 때, 그 타워 공격
        int size = towers.Count;
        for (int i = 0; i < size; i++)
        {
            if (currentTower.transform.GetComponent<Building>().unitCount >=
                towers[i].transform.GetComponent<Building>().unitCount)
            {
                return towers[i].gameObject;
            }
        }

        return towers[0].gameObject;
        //return null;
    }

    public GameObject SelectTowerToSupport()
    {
        //내 팀이 2개 이상일 때 그 카운트의 반 개만 뽑아서 가장 가까운 순서대로 maxcapacity의 25%보다 작으면 그 타워로 send unit
        int myTeamCount = 0;
        for (int i = 0; i < towers.Count; i++)
        {
            if (towers[i].transform.GetComponent<Building>().myColor == currentTower.transform.GetComponent<Building>().myColor)
            {
                myTeamCount++;
            }
        }

        if (myTeamCount > 2)
        {
            int _minUnit = 0, _currentUnit = 0;

            for (int i = 0; i < myTeamCount / 2; i++)
            {
                _currentUnit = towers[i].transform.GetComponent<Building>().unitCount;
                _minUnit = (int)(towers[i].transform.GetComponent<Building>().Capacity * 0.25f);
                if (_currentUnit <= _minUnit && currentTower.transform.GetComponent<Building>().unitCount > 5)
                {
                    return towers[i].gameObject;
                }
            }
        }
        return null;
    }

   
}
