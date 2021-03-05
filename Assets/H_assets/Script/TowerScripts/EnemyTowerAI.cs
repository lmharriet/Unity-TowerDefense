using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTowerAI : MonoBehaviour
{
    public float enemyThinkTime;
    public float rate;
    public List<GameObject> towers = new List<GameObject>();
    int myIndex;

    /*
        2.만약 팀색이 다를경우, 내가 점령이 가능한지 체크

        3.팀색이 같을 경우 
     */

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

            if (dis <= 0.1f) myIndex = i;
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
        for (int i = 0; i < distancesPair.Count; i++)
        {
            towers.Add(allTowers[distancesPair[i].Key]);
        }

        Debug.Log(towers[2].transform.GetComponent<Building>().myId);
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
                int calculateUnit = (int)(TowerManager.Instance.allTowerData[myIndex].unitCount * 0.75f);
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
        int size = towers.Count / 2;
        for (int i = 0; i < size; i++)
        {
            if (TowerManager.Instance.allTowerData[myIndex].unitCount >=
                towers[i].transform.GetComponent<Building>().unitCount)
            {
                return towers[i].gameObject;
            }
            else if (i == size - 1 && TowerManager.Instance.allTowerData[myIndex].unitCount <=
                towers[i].transform.GetComponent<Building>().unitCount)
            {
                return towers[0].gameObject;
            }
        }

        return null;
    }

    public GameObject SelectTowerToSupport()
    {
        //내 팀이 2개 이상일 때 그 카운트의 반 개만 뽑아서 가장 가까운 순서대로 maxcapacity의 25%보다 작으면 그 타워로 send unit

        return null;
    }

    public float CalculateRate()
    {
        int _ranNum = Random.Range(1, 11);

        if (_ranNum > 0 && _ranNum < 3)
        {
            rate = 0.25f;
        }
        else if (_ranNum >= 3 && _ranNum < 6)
        {
            rate = 0.5f;
        }
        else if (_ranNum >= 6 && _ranNum < 9)
        {
            rate = 0.75f;
        }
        else
        {
            rate = 1.0f;
        }
        return rate;
    }



    public void SetMushrooms(Transform _target, int i, GameObject _unit)
    {
        //int _column = 5;
        //float[] _value = { 0, 3, -3, 6, -6 };

        //Vector3 _dir = (_target.position - transform.position).normalized;
        //_dir.y = 0;

        //_unit.transform.position = transform.position;
        ////활성화시 방향을 타겟방향을 바라보고 , 그 방향에서 왼쪽 오른쪽으로 조금씩 각도를 더 틀어준다.
        ////unit이 움직일 때 transform.forward 방향으로 가면서 퍼져나가는 모양을 보여줌
        //_unit.transform.rotation = Quaternion.LookRotation(_dir, Vector3.up);
        //_unit.transform.Rotate(Vector3.up * (_value[i % _column]));

        //_unit.transform.GetComponent<UnitMove>().InitMushroom(_target, 2, myTeam);
        //_unit.SetActive(true);


        ////unit이 생성되는 tower의 unit 숫자는 감소 시켜준다.
        //unit--;
        //showUnit.text = unit.ToString();
    }

}
