using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Building : MonoBehaviour
{
    public bool isPlayerTeam;
    public EnumSpace.TEAMCOLOR myTeam;
    public EnumSpace.TOWERKIND kind;

    protected bool isSetStartStat = false;
    public int myId;
    public int unit;
    public int level = 1;
    public int maxCapacity;  // 20 40 60 80 100 수용 가능
    protected int maxLevel;
    public int upgradeCost;
    public Renderer render;
    public TextMesh showUnit;
    public Image[] upgradeImg;

    //enemy AI 
    EnemyTowerAI enemyAi;
    GameObject targetTowerofEnemy;
    public float enemyThinkTime;
    public float delay;
    float rate;
    //enumy 상태 - > 타워 범위 확장 , 공격 , 내 팀 지원

    List<GameObject> act;
    GameObject[] objs;
    protected virtual void Awake()
    {
        if (transform.GetComponentInChildren<TextMesh>() != null)
        {
            showUnit = transform.GetComponentInChildren<TextMesh>();
        }
        render = transform.GetComponent<Renderer>();
        render.material.color = TowerManager.Instance.GetColor(myColor);

        enemyAi = GameObject.Find("EnemyAI").GetComponent<EnemyTowerAI>();

        act = new List<GameObject>();
        objs = new GameObject[3];
    }

    protected virtual void Start()
    {
        SetStatByLevel();
        if (myColor != EnumSpace.TEAMCOLOR.NONE)
        {
            TowerManager.Instance.teamTowerCount[myColor] = 1;
        }

        isSetStartStat = true;
        delay = 3f;

    }

    protected abstract void SetStatByLevel();


    protected virtual void Update()
    {
        //user team이 아닐 때
        if (!isPlayerTeam && myColor != EnumSpace.TEAMCOLOR.NONE)
        {
            //enemyThinkTime += Time.deltaTime;

            EnemyAI();
        }
    }

    public GameObject AddEnemyAction()
    {
        act.Clear();
        objs[0] = enemyAi.SelectTowerToOccupy();
        objs[1] = enemyAi.SelectTowerToSupport();
        objs[2] = enemyAi.SelectTowerToAttack();

        //List<int> l = new List<int>(); 디버깅용
        for (int i = 0; i < 3; i++)
        {
            if (objs[i] != null)
            {
                act.Add(objs[i]);
                //l.Add(i); 디버깅용
            }
        }

        int idx = Random.Range(0, act.Count);

        //switch (l[idx]) 디버깅용
        //{
        //    case 0:
        //        Debug.Log("점령!");
        //        break; 
        //    case 1:
        //        Debug.Log("지원!");
        //        break;
        //    case 2:
        //        Debug.Log("공격!");
        //        break;
        //}

        return act[idx];
    }

    public void EnemyAI()
    {
        enemyThinkTime += Time.deltaTime;

        if (enemyThinkTime > delay)
        {
            enemyThinkTime = 0f;

            enemyAi.SortTowersByDistance(TowerManager.Instance.allTowers, transform);
            targetTowerofEnemy = AddEnemyAction();

            int _size = (int)(unit * CalculateRate());

            for (int i = 0; i < _size; i++)
            {
                GameObject _unit = ObjectPool.instance.GetObjectFromPooler("Unit");
                if (_unit != null)
                {
                    SetMushrooms(targetTowerofEnemy.transform, i, _unit);
                }

            }

            delay = Random.Range(3f, 7f);
        }

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

    public void CheckAttack(EnumSpace.TEAMCOLOR unitColor)
    {
        //타워에 부딪힌 유닛 색과 타워 색이 같으면
        if (unitColor == myColor)
        {
            unit++;
            showUnit.text = "p" + unit.ToString();

        }
        //타워에 부딪힌 유닛 색과 타워 색이 다르면
        else
        {
            unit--;
            //만약 타워가 가진 유닛의 개체수가 0보다 작아지면
            if (unit <= 0)
            {
                unit = 0;
                myColor = unitColor;    //현재 타워의 팀을 마지막으로 공격한 unit 팀으로 변경
                render.material.color = TowerManager.Instance.GetColor(myColor);
                //현재 타워의 팀 변경
                //transform.parent = TowerData.Instance.team.transform.GetChild((int)myColor);

                //만약 타워가 플레이어팀이었으면 현재는 점령당했으므로 플레이어팀이 아니고
                if (isPlayerTeam) isPlayerTeam = false;
                else
                {
                    //Debug.Log(myId);
                    //타워가 플레이어팀이 아니었으면 현재 컬러가 플레이어팀과 같은지 체크하고 
                    //플레이어팀과 같을 때 플레이어팀으로 변경
                    if (myColor == TowerManager.Instance.playerColor)
                    {
                        // Debug.Log("플레이어팀");
                        isPlayerTeam = true;
                    }
                }

            }

            if (isPlayerTeam)
                showUnit.text = "P" + unit.ToString();
            else
                showUnit.text = unit.ToString();
        }
    }

    protected virtual void SetMushrooms(Transform _target, int i, GameObject _unit)
    {
        int _column = 5;
        float[] _value = { 0, 3, -3, 6, -6 };

        Vector3 _dir = (_target.position - transform.position).normalized;
        _dir.y = 0;

        _unit.transform.position = transform.position;
        //활성화시 방향을 타겟방향을 바라보고 , 그 방향에서 왼쪽 오른쪽으로 조금씩 각도를 더 틀어준다.
        //unit이 움직일 때 transform.forward 방향으로 가면서 퍼져나가는 모양을 보여줌
        _unit.transform.rotation = Quaternion.LookRotation(_dir, Vector3.up);
        _unit.transform.Rotate(Vector3.up * (_value[i % _column]));

        _unit.transform.GetComponent<UnitMove>().InitMushroom(_target, 2, myTeam);
        _unit.SetActive(true);


        //unit이 생성되는 tower의 unit 숫자는 감소 시켜준다.
        unit--;
        showUnit.text = unit.ToString();
    }

    public void ChceckWinner()
    {
        if (myColor != EnumSpace.TEAMCOLOR.NONE)
        {
            if (isPlayerTeam)
            {
                if (TowerManager.Instance.teamTowerCount.ContainsKey(myColor))
                {
                    //Debug.Log("player"+myColor);
                }
            }
            else
            {
                if (TowerManager.Instance.teamTowerCount.ContainsKey(myColor))
                {
                    // Debug.Log("enemy"+myColor);

                }
            }

        }
    }

    public int unitCount
    {
        get { return unit; }
        set { unit = value; }
    }
    public EnumSpace.TEAMCOLOR myColor
    {
        get { return myTeam; }
        set { myTeam = value; }
    }
    public int TowerLever
    {
        get { return level; }
        set
        {
            if (level < maxLevel)
            {
                level = value;
                SetStatByLevel();

            }
        }
    }

    public int Cost
    {
        get { return upgradeCost; }
    }
    public int Capacity
    {
        get
        {
            if (kind == EnumSpace.TOWERKIND.TOWN)
                return maxCapacity;
            return 50;
        }
    }
}
