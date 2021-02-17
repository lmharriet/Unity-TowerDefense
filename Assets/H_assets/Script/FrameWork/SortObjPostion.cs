using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class SortObjPostion : MonoBehaviour
{
    //public GameObject testoObj;

    //public Transform center;
    //private Vector3 centerVector;

    //private List<GameObject> objs = new List<GameObject>();

    //public int row = 2;
    //public int maxCount = 10;
    //public float distance = 2f;


    public int column;
    public int size; //생성 갯수
    public float newDistance;
    public Transform centerPos;
    public Vector3 centerVec;
    public GameObject unit;

    // Start is called before the first frame update
    void Start()
    {
        centerVec = centerPos.position;

        //  centerVector = center.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SortUnits();
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


    //public void CreateTest()
    //{
    //    if (objs.Count < maxCount)
    //    {
    //        GameObject save = Instantiate(testoObj);
    //        save.name = Random.Range(1, 100).ToString();
    //        save.SetActive(true);
    //        AddItem(save);
    //    }
    //}


    //public void AddItem(GameObject item)
    //{
    //    if (maxCount > objs.Count && !objs.Contains(item))
    //    {
    //        objs.Add(item);
    //        UpdateSort();
    //    }
    //}

    //public void RemoveAll()
    //{
    //    objs.Clear();
    //}

    //public void RemoveItem(GameObject item)
    //{
    //    if (objs.Contains(item))
    //    {
    //        objs.Remove(item);
    //        UpdateSort();
    //    }
    //}

    ////기준점에서 x,z배열로 정렬
    //private void UpdateSort()
    //{
    //    float xOffset = centerVector.x;
    //    float zOffset = centerVector.z;

    //    for (int i = 0; i < objs.Count; i++)
    //    {
    //        if (i % row == 0 && i != 0)
    //        {
    //            xOffset = centerVector.x;
    //            zOffset += distance;
    //        }
    //        objs[i].transform.position = new Vector3(xOffset + distance * (i % row),
    //            centerVector.y, zOffset);
    //    }
    //}
}
