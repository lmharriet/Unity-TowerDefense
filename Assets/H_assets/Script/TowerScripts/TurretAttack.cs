using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttack : MonoBehaviour
{
    public GameObject target;
    public Transform firePoint;
    public EnumSpace.TEAMCOLOR towerColor;
    public EnumSpace.TEAMCOLOR unitColor;
    public bool isAttack;

    Ray ray;
    RaycastHit hit;


    public Vector3 direction;

    public float t = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //collider
        Collider[] cols = Physics.OverlapSphere(transform.position + Vector3.down, 8f);

        bool check = false;
        for (int i = 0; i < cols.Length; i++)
        {
            Collider col = cols[i];
            if (col.CompareTag("Unit"))
            {
                unitColor = col.transform.GetComponent<UnitMove>().GetUnitColor();

                if (Physics.Raycast(transform.position, Vector3.down, out hit, 3f))
                {
                    if (hit.transform.CompareTag("Tower"))
                    {
                        towerColor = hit.transform.GetComponent<Building>().myColor;
                    }
                }

                if (unitColor != towerColor)
                {
                    //Debug.Log("유닛 색: " + unitColor);
                    //Debug.Log("현재 타워 색 :" + towerColor);

                    target = col.transform.GetComponent<UnitMove>().gameObject;
                    check = true;
                    break;
                }

            }
        }
        //하나의 유닛도 발견되지 않으면
        if (check) isAttack = true;
        else isAttack = false;


        if (isAttack)
        {
            t += Time.deltaTime;
            direction = (target.transform.position - transform.position).normalized;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);

            if (t > 0.8f)
            {
                t = 0;
                Attack();
            }
        }

    }

    public void Attack()
    {
        GameObject _bomb = ObjectPool.instance.GetObjectFromPooler("Bomb");
        _bomb.transform.position = firePoint.position;
        _bomb.SetActive(true);
        Rigidbody rigid = _bomb.GetComponent<Rigidbody>();

        rigid.velocity = CalculateVelocity(target.transform.position, firePoint.position, .5f);
        
    }

    private Vector3 CalculateVelocity(Vector3 targetPos, Vector3 origin, float time)
    {
        Vector3 vec = targetPos - origin; // 크기와 방향

        Vector3 vecXZ = vec;
        vecXZ.y = 0;
        //크기, 방향에서 y가 0인 벡터

        float yValue = vec.y;            //크기, 방향 벡터에서의 높이
        float xzValue = vecXZ.magnitude; //거리만 뽑음 (y = 0)

        float xzVelocity = xzValue / time; // 거리 / 시간 = 속력

        float yVelocity = yValue / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;
        //disctance / time + 1/2 * gravity * time

        Vector3 result = vecXZ.normalized; // 방향 (y = 0)
        result *= xzVelocity;
        result.y = yVelocity;

        return result;
    }

    public bool Attackable
    {
        get { return isAttack; }
        set
        {
            isAttack = value;
        }
    }

    //안에 유닛이 있는지 없는지 어케확인해?

}
