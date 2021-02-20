using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class SortObjPostion : MonoBehaviour
{
    public bool lineSort;
    public bool roundSort;
    public int column;
    public int size; //생성 갯수
    public float newDistance;
    public Transform centerPos;
    public Vector3 centerVec;
    public GameObject unit;

    public GameObject depart;
    public GameObject arrive;

    // Start is called before the first frame update
    void Start()
    {
        centerVec = centerPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (lineSort && Input.GetMouseButtonDown(0))
        {
            // SortUnits();
            SetRotation();
        }


    }

    public void SortUnits()
    {
        //기준점이 되는 위치를 한 행의 전체 열에서 가운데로 맞춘다.
        float x = centerPos.position.x - (column / 2) * newDistance;
        float z = centerPos.position.z;

        if (size < column) //전체 사이즈가 내가 지정한 맥스 열보다 작을 때 
        {
            for (int i = 0; i < size; i++)
            {
                GameObject _unit = Instantiate(unit);
                _unit.transform.position =
                    new Vector3(x + i * newDistance, centerPos.position.y, z);
                _unit.transform.rotation = Quaternion.identity;



                _unit.SetActive(true);
            }
        }
        else //전체 사이즈가 내가 지정한 맥스 열보다 클 때, (행을 바꿔줘야 한다)
        {
            for (int i = 0; i < size; i++)
            {
                GameObject _unit = Instantiate(unit);
                _unit.transform.position =
                    new Vector3(x + (i % column) * newDistance, centerPos.position.y, z - (i / column) * newDistance);
                _unit.transform.rotation = Quaternion.identity;
                _unit.SetActive(true);
            }
        }


    }

    public void SetRotation()
    {
        Vector3 dir = arrive.transform.position - depart.transform.position;

        float[] value = { 0f, 5f, -5f, 10f, -10f };
        for (int i = 0; i < 5; i++)
        {
            GameObject _unit = Instantiate(unit);

            _unit.transform.position = depart.transform.position;
            _unit.transform.rotation = Quaternion.LookRotation(dir);
            _unit.transform.Rotate(Vector3.up * (i * 2));
        }
    }

}


/*    
      //{
      //    float _x = departPos.x - (column / 2) * unitDistance;
      //    float _z = departPos.z;

      //    //열 맞춰 생성
      //    _unit.transform.position = new Vector3(_x + (i % column) * unitDistance, departPos.y,
      //            _z - (i / column) * unitDistance);
      _unit.transform.position = TowerManager.Instance.GetDepartPos();
      _unit.transform.rotation = Quaternion.identity;
      _unit.transform.GetComponent<UnitMove>().InitMushroom(target, 2f, TowerManager.Instance.departTower.myTeam);
      _unit.SetActive(true);

      //unit이 생성되는 tower의 unit 숫자는 감소 시켜준다.
      TowerManager.Instance.departTower.unitCount--;
    
*/