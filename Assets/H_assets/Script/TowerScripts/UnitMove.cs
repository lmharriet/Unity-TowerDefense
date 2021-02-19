using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMove : MonoBehaviour
{
    public EnumSpace.TEAMCOLOR unitColor;
    public Renderer render;
    Transform target;
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
            Vector3 _direction = (target.transform.position - transform.position).normalized;
            float _distance = Vector3.Distance(target.transform.position, transform.position);

            if (_distance >= 0.5f)
            {
                transform.Translate(_direction * speed * Time.deltaTime);
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
