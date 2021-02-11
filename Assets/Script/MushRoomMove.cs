using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushRoomMove : MonoBehaviour
{
    Rigidbody rigid;
    Transform target;
    float speed;

    private void Awake()
    {
        rigid = transform.GetComponent<Rigidbody>();
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
                //cc.Move(_direction * speed * Time.deltaTime);
            }
        }
    }

    public void InitMushroom(Transform targetPos, float moveSpeed)
    {
        target = targetPos;
        speed = moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name.Equals(target.transform.name) 
            && other.CompareTag("Tower"))
        {
            gameObject.SetActive(false);
        }
    }
}
