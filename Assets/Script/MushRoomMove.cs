using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushRoomMove : MonoBehaviour
{
    CharacterController cc;
    Transform target;
    float speed;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();

    }

    void Update()
    {
        if (target != null)
        {
            Vector3 _direction = (target.transform.position - transform.position).normalized;
            float _distance = Vector3.Distance(target.transform.position, transform.position);

            if (_distance >= 0.5f)
            {
                cc.Move(_direction * speed * Time.deltaTime);
            }
        }
    }

    public void InitMushroom(Transform targetPos, float moveSpeed)
    {
        target = targetPos;
        speed = moveSpeed;
    }

}
