using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMove : MonoBehaviour
{
    public EnumSpace.TEAMCOLOR unitColor;
    public Renderer render;
    public Transform target;
    Vector3 spawnPos;
    Vector3 turnDir;
    float halfDistance;
    int targetId;
    float speed;

    private void Awake()
    {
        render = transform.GetComponent<Renderer>();
    }

    void Update()
    {
        if (target != null)
        {

            float _dis = Vector3.Distance(spawnPos, transform.position);

            //스폰된 위치로부터 타겟위치까지 거리의 절반이 
            //스폰된 위치에서 현재위치의 거리보다 크거나 같으면 
            if (_dis <= halfDistance)
            {
                //생성시 부여받은 transform의 로컬 forward방향으로 이동
                transform.Translate(Vector3.forward * speed * Time.deltaTime);

                //tartget까지의 방향을 계속 저장, 첫 스폰위치에서 타겟타워까지 거리의 반에 도달했을 때 
                //저장되는 방향이 최종 방향이 된다.
                turnDir = (target.position - transform.position).normalized;

            }

            else
            {
                transform.Translate(turnDir * speed * Time.deltaTime, Space.World);
            }
        }
    }

    public void InitMushroom(Transform targetPos, float moveSpeed, EnumSpace.TEAMCOLOR unit_Color)
    {
        target = targetPos;
        speed = moveSpeed;
        targetId = target.transform.GetComponent<Building>().myId;
        unitColor = unit_Color;
        render.material.color = TowerManager.Instance.GetColor(unitColor);

        //타겟타워까지 퍼져서 움직였다가 모아지는 연출을 위함
        spawnPos = transform.position;
        halfDistance = Vector3.Distance(target.position, transform.position) * 0.5f;
    }

    public EnumSpace.TEAMCOLOR GetUnitColor()
    {
        return unitColor;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            Building _tower = other.transform.GetComponent<Building>();

            //충돌한 타워의 id와 도착 타워의 id가 일치할 때
            if (_tower.myId == targetId)
            {
                //타워 어택
                _tower.CheckAttack(unitColor);

               
                gameObject.SetActive(false);
            }

        }

    }

}
