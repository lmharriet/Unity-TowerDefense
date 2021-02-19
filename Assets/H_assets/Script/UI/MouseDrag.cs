using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MouseDrag : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    private Ray rayR;
    private RaycastHit hitR;

    public Image img;            //drag 이미지
    private Vector3 startPos;
    private Vector3 departure;
    private Vector3 arrive;


    public GameObject unitPref;

    private Transform sortCenter;
    private Vector3 sortVector;
    public int column;
    public float unitDistance;
    public bool isMultiSelected = false;
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


            if (TowerManager.Instance.departTower != null)
            {
                //myTower = null;
                //towardTower = null;
                TowerManager.Instance.ResetBothTowers();
            }

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Tower"))
                {
                    if (hit.transform.GetComponent<Building>().isPlayerTeam)
                    {
                        Debug.Log(hit.transform.name);

                        //myTower = hit.transform.gameObject;
                        TowerManager.Instance.SetDepartTower(hit);
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

            if (Input.GetMouseButtonDown(1))
            {
                rayR = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(rayR, out hitR) && hitR.transform.CompareTag("Tower"))
                {
                    if (hitR.transform.GetComponent<Building>().isPlayerTeam)
                    {
                        //list에 hitR에 저장된 gameobject가 없으면
                        if (!TowerManager.Instance.departTowers.Contains(hitR.transform.GetComponent<Building>()))
                        {
                            //list에 gameobject를 담아준다
                            TowerManager.Instance.departTowers.Add(hitR.transform.GetComponent<Building>());
                        }

                        Debug.Log(TowerManager.Instance.departTowers.Count);
                    }
                }
            }
        }

        //드래그 완료
        if (Input.GetMouseButtonUp(0))
        {
            //도착 위치
            arrive = Input.mousePosition;
            img.gameObject.SetActive(false);

            if (TowerManager.Instance.departTowers.Count != 0)
            {
                isMultiSelected = true;
            }

            if (TowerManager.Instance.departTower != null)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    //raycast 안에 정보가 하나라도 있으면 if안에 들어온다( any object ).

                    if (hit.transform.CompareTag("Tower"))
                    {
                        int _departId = TowerManager.Instance.departTower.myId;
                        int _arriveId = hit.transform.GetComponent<Building>().myId;
                        if (_departId != _arriveId)
                        {
                            TowerManager.Instance.SetArriveTower(hit);
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

                if (TowerManager.Instance.arriveTower == null)
                {
                    TowerManager.Instance.departTower = null;
                    TowerManager.Instance.departTowers.Clear();
                }
            }

        }


        //유닛이 도착할 타워가 지정 됐을 때
        if (TowerManager.Instance.arriveTower != null)
        {
            if (isMultiSelected) //타워가 멀티로 선택 되었으면?
            {
                SendUnit(0.5f);
                SendUnitFromMultipleTowers(0.5f);

                //병력을 보내고 나면 myTower와 towardTower 컨테이너 비우고 multiselect ->false
                TowerManager.Instance.ResetBothTowers();
                TowerManager.Instance.departTowers.Clear();
                isMultiSelected = false;
            }
            else
            {
                SendUnit(0.5f);

                //병력을 보내고 나면 myTower와 towardTower 컨테이너 비우기
                TowerManager.Instance.ResetBothTowers();
            }


        }

    }

    public void SendUnit(float percentage)
    {
        //출발하는 타워에 저장된 병사의 수
        int _size = TowerManager.Instance.departTower.unit;

        // (25%,50%,75%,100%) UI에서 세팅한 percentage에 맞춰 병력을 보내기 위한 용도
        _size = (int)(_size * percentage);
        column = 5;
        unitDistance = 1f;

        Vector3 departPos = TowerManager.Instance.GetDepartPos();
        Vector3 arrivePos = TowerManager.Instance.GetArrivePos();
        Transform target = TowerManager.Instance.arriveTower.transform;

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
                _unit.transform.GetComponent<UnitMove>().InitMushroom(target, 2f, TowerManager.Instance.departTower.myTeam);
                _unit.SetActive(true);

                //unit이 생성되는 tower의 unit 숫자는 감소 시켜준다.
                TowerManager.Instance.departTower.unitCount--;
                
            }
        }
    }

    public void SendUnitFromMultipleTowers(float percentage)
    {
        //출발하는 타워에 저장된 타워의 수
        int _size = TowerManager.Instance.departTowers.Count;
        Vector3 _arrivePos = TowerManager.Instance.GetArrivePos();
        Transform target = TowerManager.Instance.arriveTower.transform;
        column = 5;
        unitDistance = 1f;

        for (int i = 0; i < _size; i++)
        {
            int _myUnit = TowerManager.Instance.departTowers[i].unit;
            _myUnit = (int)(_myUnit * percentage);

            Vector3 departPos = TowerManager.Instance.departTowers[i].transform.position;

            for (int j = 0; j < _myUnit; j++)
            {
                GameObject _unit = ObjectPool.instance.GetObjectFromPooler("Unit");
                if (_unit != null)
                {
                    //***열에 맞춰서 position세팅을 바꿔야함.
                    //_unit.transform.position = departPos;

                    float _x = departPos.x - (column / 2) * unitDistance;
                    float _z = departPos.z;
                    //열 맞춰 생성
                    _unit.transform.position = new Vector3(_x + (j % column) * unitDistance, departPos.y,
                            _z - (j / column) * unitDistance);

                    _unit.transform.rotation = Quaternion.identity;
                    _unit.transform.GetComponent<UnitMove>().InitMushroom(target, 2f, TowerManager.Instance.departTowers[i].myTeam);
                    _unit.SetActive(true);

                    //unit이 생성되는 tower의 unit 숫자는 감소 시켜준다.
                    TowerManager.Instance.departTowers[i].unitCount--;
                    //TowerData.Instance.departTower.showUnit.text ="P"+
                    //TowerData.Instance.departTower.unitCount.ToString();

                }
            }
        }


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
