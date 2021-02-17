using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool isPlayerTeam;
    public string kind;
    public int myId;
    public int unit;
    public int level = 1;
    public int upgradeCost;
    public Renderer render;
    public TextMesh showUnit;

    public float enemyThinkTime;
    public float rate;

    public EnumSpace.TEAMCOLOR teamColor = EnumSpace.TEAMCOLOR.NONE;

    public int unitCount
    {
        get { return unit; }
        set { unit = value; }
    }
    protected virtual void Awake()
    {
        showUnit = transform.GetComponentInChildren<TextMesh>();
        render = transform.GetComponent<Renderer>();
    }
    protected virtual void Start()
    {


    }

    protected virtual void Update()
    {
        enemyThinkTime += Time.deltaTime;
        //user team이 아닐 때
        if (!isPlayerTeam)
        {
            EnemyAI();
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

    public void EnemyAI()
    {
  
        if (myColor != EnumSpace.TEAMCOLOR.NONE)
        {
            List<int> num = new List<int>();
            Vector3 _myPos = transform.position;
            Transform _target;
            float _delay = Random.Range(3f, 7f);
            if (enemyThinkTime > _delay)
            {
                enemyThinkTime = 0f;
                for (int i = 0; i < TowerData.Instance.maxTower; i++)
                {
                    if (Vector3.Distance(TowerData.Instance.allTowers[i].transform.position, _myPos) <= 0.1f) continue;

                    num.Add(i);
                }

                int _rnd = Random.Range(0, num.Count);
                _target = TowerData.Instance.allTowers[_rnd].transform;

                int _size = (int)(unit * CalculateRate());

                for (int i = 0; i < _size; i++)
                {
                    GameObject _unit = ObjectPool.instance.GetObjectFromPooler("Unit");
                    if (_unit != null)
                    {
                        SetMushrooms(_target, i, _unit);

                    }

                }
            }

        }

    }

    protected virtual void SetMushrooms(Transform _target, int i, GameObject _unit)
    {
        //***열에 맞춰서 position세팅을 바꿔야함.
        //_unit.transform.position = departPos;

        int _column = 5;
        float _unitDistance = 1f;

        float _x = transform.position.x - (_column / 2) * _unitDistance;
        float _z = transform.position.z;
        //열 맞춰 생성
        _unit.transform.position = new Vector3(_x + (i % _column) * _unitDistance, transform.position.y,
                _z - (i / _column) * _unitDistance);

        _unit.transform.rotation = Quaternion.identity;
        _unit.transform.GetComponent<UnitMove>().InitMushroom(_target, 2f, teamColor);
        _unit.SetActive(true);

        //unit이 생성되는 tower의 unit 숫자는 감소 시켜준다.
        unit--;
        showUnit.text = teamColor + unit.ToString();
    }

    public void CheckAttack(EnumSpace.TEAMCOLOR unitColor)
    {
        //타워에 부딪힌 유닛 색과 타워 색이 같으면
        if (unitColor == myColor)
        {
            unit++;
            if (isPlayerTeam)
                showUnit.text = "p" + unit.ToString();
            else
                showUnit.text = teamColor.ToString() + unit.ToString();

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
                render.material.color = TowerData.Instance.GetColor(myColor);
                //현재 타워의 팀 변경
                transform.parent = TowerData.Instance.team.transform.GetChild((int)myColor);

                //만약 타워가 플레이어팀이었으면 현재는 점령당했으므로 플레이어팀이 아니고
                if (isPlayerTeam) isPlayerTeam = false;
                else
                {
                    //타워가 플레이어팀이 아니었으면 현재 컬러가 플레이어팀과 같은지 체크하고 
                    //플레이어팀과 같을 때 플레이어팀으로 변경
                    if (myColor == TowerData.Instance.playerColor) isPlayerTeam = true;
                }

            }


            if (isPlayerTeam)
                showUnit.text = "p" + unit.ToString();
            else
                showUnit.text = teamColor.ToString() + unit.ToString();
        }
    }

    public EnumSpace.TEAMCOLOR myColor
    {
        get { return teamColor; }
        set { teamColor = value; }
    }


}
