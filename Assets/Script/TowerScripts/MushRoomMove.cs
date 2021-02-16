using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushRoomMove : MonoBehaviour
{
    public BuildingManager.TEAMCOLOR unitColor;
    public Renderer render;
    Rigidbody rigid;
    Transform target;
    int targetId;
    float speed;

    private void Awake()
    {
        rigid = transform.GetComponent<Rigidbody>();
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
                //cc.Move(_direction * speed * Time.deltaTime);
            }
        }

    }

    public void InitMushroom(Transform targetPos, float moveSpeed, BuildingManager.TEAMCOLOR unit_Color)
    {
        target = targetPos;
        speed = moveSpeed;
        targetId = target.transform.GetComponent<BuildingManager>().myId;
        unitColor = unit_Color;
        render.material.color = TowerData.Instance.GetColor(unitColor);
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            BuildingManager _tower = other.transform.GetComponent<BuildingManager>();

            //충돌한 타워의 id와 도착 타워의 id가 일치할 때
            if (_tower.myId == targetId)
            {

                _tower.CheckAttack(unitColor);
                //if (unitColor == _tower.teamColor)
                //{
                //    _tower.unitCount++;
                //    _tower.showUnit.text = "P" + _tower.unitCount.ToString();
                //    Debug.Log( "나는"+ unitColor);
                //    Debug.Log(_tower.teamColor + "타워 유닛 ++");
                //}
                //else
                //{
                //    _tower.unitCount--;
                //    _tower.showUnit.text = _tower.teamColor.ToString() + _tower.unitCount.ToString();
                //    Debug.Log( "나는"+ unitColor);
                //    Debug.Log(_tower.teamColor + "타워 유닛 --");
                //}
            }

            gameObject.SetActive(false);


        }




        ////unit이 부딪힌 게임오브젝트가 tower일 때,
        //if (other.CompareTag("Tower"))
        //{
        //    BuildingManager.TEAMCOLOR _towerColor = other.transform.GetComponent<BuildingManager>().teamColor;
        //    BuildingManager _tower = other.transform.GetComponent<BuildingManager>();

        //    //target tower의 id와 같은 id를 갖고 있으면
        //    if (_tower.myId == targetId)
        //    {
        //        if (_towerColor == unitColor)
        //        {
        //            //player team이면 unit count 증가
        //            _tower.unitCount++;
        //            _tower.showUnit.text = "P" + _tower.unitCount.ToString();
        //            //  Debug.Log(_tower.unitCount);
        //        }

        //        else
        //        {
        //            //player team이 아니면 unit count 감소
        //            _tower.unitCount--;
        //            _tower.showUnit.text = _towerColor + _tower.unitCount.ToString();
        //            //Debug.Log(_tower.unitCount);
        //        }

        //        gameObject.SetActive(false);
        //    }
        //}
    }




}
