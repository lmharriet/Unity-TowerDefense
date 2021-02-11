using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MouseDrag : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    public Image img;
    private Vector3 startPos;
    private Vector3 departure;
    private Vector3 arrive;
    public GameObject myTower;
    public GameObject towardTower;

    public GameObject unitPref;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //드래그 시작위치
        if (Input.GetMouseButtonDown(0))
        {
            img.gameObject.SetActive(true);
            startPos = Input.mousePosition;
            //출발 위치
            departure = startPos;
            img.transform.position = startPos;

            if (myTower != null)
            {
                myTower = null;
                towardTower = null;
            }

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Tower"))
                {
                    myTower = hit.transform.gameObject;
                }
            }

        }



        //드래그 중
        if (Input.GetMouseButton(0))
        {
            Vector3 currentPos = Input.mousePosition;
            img.transform.localScale = new Vector2(Vector3.Distance(currentPos, startPos), 1);
            img.transform.localRotation = Quaternion.Euler(0, 0, AngleInDegree(startPos, currentPos));
        }
        //드래그 완료
        if (Input.GetMouseButtonUp(0))
        {
            //도착 위치
            arrive = Input.mousePosition;
            img.gameObject.SetActive(false);



            if (myTower != null && myTower.GetComponent<BuildingManager>().isPlayerTeam)
            {

                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    //raycast 안에 정보가 하나라도 있으면 if안에 들어온다( any object ).

                    if (hit.transform.CompareTag("Tower"))
                    {
                        towardTower = hit.transform.gameObject;
                    }
                    //else
                    //{
                    //    myTower = null;
                    //}

                }
                //else
                //{
                //raycast 안에 어떤 정보도 없을 때( 하늘? )
                //    myTower = null; 
                //}

                if (towardTower == null) myTower = null;
            }



        }


        if (towardTower != null)
        {
            ////유닛 생성
            //var obj =Instantiate(unitPref, myTower.transform.position, Quaternion.identity);
            //obj.transform.GetComponent<MushRoomMove>().InitMushroom(towardTower.transform, 2);
            //myTower = null;
            //towardTower = null;
        
            GameObject _unit = ObjectPool.instance.GetObjectFromPooler("Unit");
            if (_unit != null)
            {
                _unit.transform.position = myTower.transform.position;
                _unit.transform.rotation = Quaternion.identity;
                _unit.transform.GetComponent<MushRoomMove>().InitMushroom(towardTower.transform, 2);
                _unit.SetActive(true);
               // Debug.Log("생성");

            }
            myTower = null;
            towardTower = null;
        }
    }

    public static float AngleInRadius(Vector3 vec1, Vector3 vec2)
    {
        return Mathf.Atan2(vec2.y - vec1.y, vec2.x - vec1.x);
    }

    public static float AngleInDegree(Vector3 vec1, Vector3 vec2)
    {
        return AngleInRadius(vec1, vec2) * 180 / Mathf.PI;
    }


}
