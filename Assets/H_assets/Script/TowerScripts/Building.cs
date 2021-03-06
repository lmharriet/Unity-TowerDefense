﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;

public abstract class Building : MonoBehaviour
{
    public bool isGameOver;
    public bool isPlayerTeam;
    public EnumSpace.TEAMCOLOR myTeam;
    public EnumSpace.TOWERKIND kind;
    public EnumSpace.TEAMCOLOR factoryColor;
    public EnumSpace.TEAMCOLOR winner;
    public List<EnumSpace.TEAMCOLOR> LoserTeams;
    public GameObject flagPreb;
    public Transform flagPos;

    protected bool isSetStartStat = false;
    public int myId;
    public int unit;
    public int level = 1;
    public int maxCapacity;  // 20 40 60 80 100 수용 가능
    protected int maxLevel;
    public int upgradeCost;

    public int column;
    //factory tower가 내 팀에있을 경우 없을 경우 방어, 공격
    protected float defTime;
    protected float boostTime;
    protected int amount;

    //test
    public ParticleSystem dummypar;
    public ParticleSystem dummypar2;
    //test


    public GameObject effectCircle;
    public TextMesh showUnit;
    public Image[] upgradeImg;

    //public Renderer render;

    //enemy AI 
    EnemyTowerAI enemyAi;
    GameObject targetTowerofEnemy;
    public float enemyThinkTime;
    public float delay;
    float rate;
    //enumy 상태 - > 타워 범위 확장 , 공격 , 내 팀 지원

    List<GameObject> act;
    GameObject[] objs;

    Coroutine shield;
    Coroutine critical;

    protected virtual void Awake()
    {
        enemyAi = GameObject.Find("EnemyAI").GetComponent<EnemyTowerAI>();

        act = new List<GameObject>();
        objs = new GameObject[3];
        LoserTeams = new List<EnumSpace.TEAMCOLOR>();
        dummypar = GameObject.Find("dummydefPar").GetComponent<ParticleSystem>();
        dummypar2 = GameObject.Find("dummyatkPar").GetComponent<ParticleSystem>();
    }

    protected virtual void Start()
    {

        SetStatByLevel();

        if (myColor != EnumSpace.TEAMCOLOR.NONE)
        {
            ActiveFlag(myId, myColor);
            TowerManager.Instance.teamTowerCount[myColor] = 1;
        }

        if (kind == EnumSpace.TOWERKIND.FACTORY)
        {
            TowerManager.Instance.factoryColor = myColor;
            // Debug.Log(TowerManager.Instance.factoryColor);
        }
        isSetStartStat = true;
        delay = 3f;

    }

    protected abstract void SetStatByLevel();



    protected virtual void Update()
    {
        if (isGameOver)
        {
            this.enabled = true;
        }
        else
        {
            //user team이 아닐 때
            if (!isPlayerTeam && myColor != EnumSpace.TEAMCOLOR.NONE)
            {
                //enemyThinkTime += Time.deltaTime;

                EnemyAI();
            }
        }

    }

    protected void SetTextMesh()
    {
        if (isPlayerTeam)
        {
            showUnit.gameObject.SetActive(true);
            showUnit.transform.GetChild(0).gameObject.SetActive(false);
            showUnit.transform.GetChild(1).gameObject.SetActive(true);
            showUnit.text = unit.ToString();
        }
        else if (myTeam == EnumSpace.TEAMCOLOR.NONE)
        {
            showUnit.gameObject.SetActive(true);
            showUnit.transform.GetChild(0).gameObject.SetActive(true);
            showUnit.transform.GetChild(1).gameObject.SetActive(false);
            showUnit.text = unit.ToString();
        }
        else
        {
            showUnit.gameObject.SetActive(false);
        }
    }


    public GameObject AddEnemyAction()
    {
        act.Clear();
        objs[0] = enemyAi.SelectTowerToOccupy();
        objs[1] = enemyAi.SelectTowerToSupport();
        objs[2] = enemyAi.SelectTowerToAttack();

        for (int i = 0; i < 3; i++)
        {
            if (objs[i] != null)
            {
                act.Add(objs[i]);
            }
        }

        int idx = Random.Range(0, act.Count);

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

            //열 맞춰서 보내기 위한 용도 ex) unit이 10마리면 5마리씩 2줄로 나감
            StartCoroutine(SendUnits(_size));

            delay = Random.Range(3f, 7f);
        }

    }


    public void SetMushrooms(Transform _target, int i, GameObject _unit)
    {
        column = 5;
        float[] _value = { 0, 3, -3, 6, -6 };

        Vector3 _dir = (_target.position - transform.position).normalized;
        _dir.y = 0;

        _unit.transform.position = transform.position;
        //활성화시 방향을 타겟방향을 바라보고 , 그 방향에서 왼쪽 오른쪽으로 조금씩 각도를 더 틀어준다.
        //unit이 움직일 때 transform.forward 방향으로 가면서 퍼져나가는 모양을 보여줌
        _unit.transform.rotation = Quaternion.LookRotation(_dir, Vector3.up);
        _unit.transform.Rotate(Vector3.up * (_value[i % column]));

        _unit.transform.GetComponent<UnitMove>().InitMushroom(_target, 2, myTeam);
        _unit.SetActive(true);


        //unit이 생성되는 tower의 unit 숫자는 감소 시켜준다.
        unit--;
        if (isPlayerTeam)
            showUnit.text = unit.ToString();
    }

    IEnumerator SendUnits(int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject _unit = ObjectPool.instance.GetObjectFromPooler("Unit");
            if (_unit != null)
            {
                SetMushrooms(targetTowerofEnemy.transform, i, _unit);
            }

            if (i % column == column - 1)
            {
                yield return new WaitForSeconds(1f);
            }
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
        if (unitColor == myColor) //support
        {
            unit++;
            showUnit.text = unit.ToString();

        }
        //타워에 부딪힌 유닛 색과 타워 색이 다르면
        else
        {
            //수정 필요
            //unit -= DamageAmount(myColor);

            //temp
            DamageAmount(myTeam);
            unit--;
            //

            //만약 타워가 가진 유닛의 개체수가 0보다 작아지면
            if (unit <= 0)
            {
                unit = 0;

                //점령 당한 현재 타워의 갯수를 -- 하고 현재 타워의 개수가 0개인지 체크
                TowerManager.Instance.teamTowerCount[myColor]--;
                ChceckWinnerTeam(myColor);

                //위에서 Winner가 판별 나지 않으면 계속 진행.
                myColor = unitColor;    //현재 타워의 팀을 마지막으로 공격한 unit 팀으로 변경
                TowerManager.Instance.teamTowerCount[myColor]++;

                DeactiveFlag();
                ActiveFlag(myId, myColor);
                if (kind == EnumSpace.TOWERKIND.FACTORY)
                {
                    factoryColor = myColor;
                    TowerManager.Instance.factoryColor = myColor;
                    // Debug.Log(TowerManager.Instance.factoryColor);
                }

                //만약 타워가 플레이어팀이었으면 현재는 점령당했으므로 플레이어팀이 아님
                if (isPlayerTeam)
                {
                    isPlayerTeam = false;
                }
                else
                {
                    //타워가 플레이어팀이 아니었으면 현재 컬러가 플레이어팀과 같은지 체크하고 
                    //플레이어팀과 같을 때 플레이어팀으로 변경
                    if (myColor == TowerManager.Instance.playerColor)
                    {
                        isPlayerTeam = true;
                    }
                }

            }
            SetTextMesh();

        }
    }


    public void DamageAmount(EnumSpace.TEAMCOLOR damagedTeam)
    {//수정 필요

        int damageCal = (int)(TowerManager.Instance.atk - TowerManager.Instance.def) / 2;
        if (factoryColor == EnumSpace.TEAMCOLOR.NONE)
        {
            //normal
            // Debug.Log("factory tower가 NoneColor");
            amount = 1;
        }
        else if (damagedTeam != EnumSpace.TEAMCOLOR.NONE)
        {

            if (factoryColor == damagedTeam)
            {
                //defense
                //defTime = damageCal * Random.Range(10, 15);
                defTime = 2f;
                if (shield == null)
                {
                    shield = StartCoroutine(ActiveShield(defTime));
                }
            }
            else
            {
                //attack
                boostTime = 2f;
                if (critical == null)
                {
                    critical = StartCoroutine(ActiveCritical(boostTime));
                }
            }

        }
    }


    public void ActiveFlag(int id, EnumSpace.TEAMCOLOR color)
    {
        flagPreb = ObjectPool.instance.GetObjectFromPooler("Flag");

        //Debug.Log("activeFlag");
        if (flagPreb != null)
        {
            flagPreb.transform.position = flagPos.position;
            flagPreb.transform.localScale = new Vector3(1.7f, 1.55f, 1.7f);
            flagPreb.transform.GetComponent<FlagState>().ActiveFlag(id, color);
            flagPreb.SetActive(true);
        }

    }

    public void DeactiveFlag()
    {
        if (flagPreb != null)
        {
            if (flagPreb.transform.GetComponent<FlagState>().flagID == myId)
            {
                flagPreb.transform.GetComponent<FlagState>().DeActiveFlag();
            }
        }
    }

    public void ChceckWinnerTeam(EnumSpace.TEAMCOLOR towerColor)
    {
        //수정 필요
        if (towerColor == EnumSpace.TEAMCOLOR.NONE) return;


        if (TowerManager.Instance.teamTowerCount[towerColor] == 0)
        {
            Debug.Log(towerColor + "는 죽었다");
            TowerManager.Instance.aliveTeam[towerColor]=false;
            LoserTeams.Add(towerColor);
        }

        //if ()
        //{
          
        //}



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

    public int TowerLevel
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

    IEnumerator ActiveShield(float maintainTime)
    {
        var particle = Instantiate(dummypar, transform.position, Quaternion.identity);
        Debug.Log("shield!");

        yield return new WaitForSeconds(maintainTime);
        Debug.Log("stopShield");
        Destroy(particle);
        shield = null;
    }
    IEnumerator ActiveCritical(float boosterTime)
    {
        var particle2 = Instantiate(dummypar2, transform.position, Quaternion.identity);
        Debug.Log("cri attack!");
        yield return new WaitForSeconds(boosterTime);
        Debug.Log("cri finish");
        Destroy(particle2);
        critical = null;
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
