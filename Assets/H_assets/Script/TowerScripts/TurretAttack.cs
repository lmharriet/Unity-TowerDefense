using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttack : MonoBehaviour
{
    public GameObject target;
    public Transform firePoint;
    public GameObject bomb;
    public bool isAttack;

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
        Collider[] cols = Physics.OverlapSphere(transform.position + Vector3.down, 6);

        bool check = false;
        for(int i = 0; i < cols.Length; i++)
        {
            Collider col = cols[i];

            if (col.CompareTag("Unit"))
            {
                check = true;
                break;
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

            Vector3 _vel = CalculateVelocity(target.transform.position, firePoint.position, .5f);

            if (t > 2f)
            {
                t = 0;
                GameObject obj = Instantiate(bomb, firePoint.position, Quaternion.identity);
                obj.SetActive(true);
                Rigidbody rigid = obj.GetComponent<Rigidbody>();

                rigid.velocity = _vel;
            }
        }
       
    }

    public void Attack()
    {

    }

    private Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        Vector3 vec = target - origin; // 크기와 방향

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
