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
    //public GameObject myTower;
    //public GameObject towardTower;

    public GameObject unitPref;

    private Transform sortCenter;
    private Vector3 sortVector;
    public int column;
    public float unitDistance;
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


            if (TowerData.Instance.departTower != null)
            {
                //myTower = null;
                //towardTower = null;
                TowerData.Instance.ResetBothTowers();
            }

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Tower"))
                {
                    if (hit.transform.GetComponent<BuildingManager>().isPlayerTeam)
                    {
                        //myTower = hit.transform.gameObject;
                        TowerData.Instance.SetDepartTower(hit);
                    }

                }
            }

        }


        //드래그 중
        if (Input.GetMouseButton(0))
        {
            Vector3 currentPos = Input.mousePosition;
            //처음 클릭했을 때 마우스 좌표와 현재 마우스 좌표의 거리만큼 x scale을 변경.
            img.transform.localScale = new Vector2(Vector3.Distance(currentPos, startPos), 1);

            float _z = AngleInDegree(startPos, currentPos);

            img.transform.localRotation = Quaternion.Euler(0, 0, _z);
        }

        //드래그 완료
        if (Input.GetMouseButtonUp(0))
        {
            //도착 위치
            arrive = Input.mousePosition;
            img.gameObject.SetActive(false);



            if (TowerData.Instance.departTower != null)
            {

                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    //raycast 안에 정보가 하나라도 있으면 if안에 들어온다( any object ).

                    if (hit.transform.CompareTag("Tower"))
                    {
                        //int _myTowerId = myTower.transform.GetComponent<BuildingManager>().myId;
                        //int _hitTowerId = hit.transform.GetComponent<BuildingManager>().myId;

                        //if (_myTowerId != _hitTowerId)
                        //    towardTower = hit.transform.gameObject;


                        int _departId = TowerData.Instance.departTower.myId;
                        int _arriveId = hit.transform.GetComponent<BuildingManager>().myId;
                        if (_departId != _arriveId)
                        {
                            TowerData.Instance.SetArriveTower(hit);

                        }

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

                if (TowerData.Instance.arriveTower == null)
                    TowerData.Instance.departTower = null;
            }

        }


        //유닛이 도착할 타워가 지정 됐을 때
        if (TowerData.Instance.arriveTower != null)
        {
            SendUnit(0.5f);

        }
    }

    public void SendUnit(float percentage)
    {
        //출발하는 타워에 저장된 병사의 수
        int _size = TowerData.Instance.departTower.unit;

        // (25%,50%,75%,100%) UI에서 세팅한 percentage에 맞춰 병력을 보내기 위한 용도
        _size = (int)(_size * percentage);
        column = 5;
        unitDistance = 1f;

        Vector3 departPos = TowerData.Instance.GetDepartPos();
        Vector3 arrivePos = TowerData.Instance.GetArrivePos();
        Transform target = TowerData.Instance.arriveTower.transform;

        //_size만큼의 병력을 미리 생성된 unit중 활성화하기
        for (int i = 0; i < _size; i++)
        {
            GameObject _unit = ObjectPool.instance.GetObjectFromPooler("Unit");
            if (_unit != null)
            {
                //***열에 맞춰서 position세팅을 바꿔야함.
                //_unit.transform.position = departPos;

                float _x = departPos.x - (column / 2) * unitDistance;
                float _z = departPos.z;
                //열 맞춰 생성
                _unit.transform.position = new Vector3(_x + (i % column) * unitDistance, departPos.y,
                        _z - (i / column) * unitDistance);

                _unit.transform.rotation = Quaternion.identity;
                _unit.transform.GetComponent<MushRoomMove>().InitMushroom(target, 2);
                _unit.SetActive(true);

                //unit이 생성되는 tower의 unit 숫자는 감소 시켜준다.
                TowerData.Instance.departTower.unitCount--;
                TowerData.Instance.departTower.showUnit.text ="P"+
                   TowerData.Instance.departTower.unitCount.ToString();

            }

        }

        //병력을 보내고 나면 myTower와 towardTower 컨테이너 비우기
        TowerData.Instance.ResetBothTowers();
    }


    #region angle
    public static float AngleInRadius(Vector3 vec1, Vector3 vec2)
    {
        return Mathf.Atan2(vec2.y - vec1.y, vec2.x - vec1.x);
    }

    public static float AngleInDegree(Vector3 vec1, Vector3 vec2)
    {
        return AngleInRadius(vec1, vec2) * 180 / Mathf.PI;
    }
    #endregion

}
