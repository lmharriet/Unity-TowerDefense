using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class Building : MonoBehaviour
{
    public bool isPlayerTeam;
    public EnumSpace.TEAMCOLOR myTeam;
    public EnumSpace.TOWERKIND kind;

    public int myId;
    public int unit;
    public int level = 1;
    protected int maxLevel;
    public int upgradeCost;
    public Renderer render;
    public TextMesh showUnit;
    public Image[] upgradeImg;

    EnemyTowerAI enemyAi;

    protected bool isSetStartStat = false;


    //enumy 상태 - > 타워 범위 확장 , 공격 , 내 팀 지원


    protected virtual void Awake()
    {
        if (transform.GetComponentInChildren<TextMesh>() != null)
        {
            showUnit = transform.GetComponentInChildren<TextMesh>();
        }
        render = transform.GetComponent<Renderer>();
        render.material.color = TowerManager.Instance.GetColor(myColor);

        enemyAi = GameObject.Find("EnemyAI").GetComponent<EnemyTowerAI>();

    }

    protected virtual void Start()
    {
        SetStatByLevel();
        if (myColor != EnumSpace.TEAMCOLOR.NONE)
        {
            TowerManager.Instance.teamTowerCount[myColor] = 1;
        }
        ChceckWinner();

        isSetStartStat = true;

     
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

    public void EnemyAI()
    {
        if(Input.GetMouseButtonDown(0))
        {
            enemyAi.SortTowersByDistance(TowerManager.Instance.allTowers, transform);
            //enumyAi.SortTowersByDistance();
        }
        //List<int> num = new List<int>();

        //Vector3 _myPos = transform.position;
        //Transform _target;

        //float _delay = Random.Range(4f, 11f);
        //if (enemyThinkTime > _delay)
        //{
        //    enemyThinkTime = 0f;

        //    // Debug.Log("생각끝!");
        //    for (int i = 0; i < TowerManager.Instance.maxTower; i++)
        //    {
        //        //Debug.Log("타워선택");
        //        if (Vector3.Distance(TowerManager.Instance.allTowers[i].transform.position, _myPos) <= 0.1f) continue;
        //        //포지션 비교해서 내 타워가 있는 포지션이 아니면 넘버를 저장
        //        num.Add(i);
        //        // Debug.Log(num[i]);
        //    }
        //    //num.Sort(delegate (int a,int b )
        //    //{
        //    //    return 0;
        //    //});

        //    //나를 제외한 타워 중 한 타워를 지정
        //    int _rnd = Random.Range(0, num.Count);
        //    _target = TowerManager.Instance.allTowers[num[_rnd]].transform;

        //    //내 타워에서 보낼 유닛 수
        //    int _size = (int)(unit * CalculateRate());

        //    for (int i = 0; i < _size; i++)
        //    {
        //        GameObject _unit = ObjectPool.instance.GetObjectFromPooler("Unit");
        //        if (_unit != null)
        //        {
        //            SetMushrooms(_target, i, _unit);

        //        }

        //    }
        //}

    }

    //index, distance, unit
    public void SortTowerIndexByDistance()
    {
        List<float> distances = new List<float>();
        for (int i = 0; i < TowerManager.Instance.maxTower - 1; i++)
        {
            distances.Add( Vector3.Distance(TowerManager.Instance.allTowers[i].transform.position, transform.position));

        }


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

}
