using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseDrag : MonoBehaviour
{
    public LayerMask mask;
    private Ray ray;            //좌클릭용
    private RaycastHit hit;     //좌클릭용
    private Ray rayR;           //우클릭용
    private RaycastHit hitR;    //우클릭용

    public Image img;            //drag 이미지
    private Vector3 startPos;
    private Vector3 departure;
    private Vector3 arrive;

    public int column;
    public bool isMultiSelected = false;
    public float percentage;        //타워가 가진 유닛의 몇 퍼센트만큼 보낼지?
    private Building prevPopUpTower;

    public GameObject circle;
    void Update()
    {
        //드래그 시작
        if (Input.GetMouseButtonDown(0))
        {
            img.gameObject.SetActive(true);
            startPos = Input.mousePosition;
            //출발할 타워 저장
            SaveDepartTower();

        }

        //드래그 중
        if (Input.GetMouseButton(0))
        {
            Vector3 currentPos = Input.mousePosition;
            //처음 클릭했을 때의 마우스 좌표와 현재 마우스 좌표의 거리만큼 x scale을 변경.
            img.transform.localScale = new Vector2(Vector3.Distance(currentPos, startPos), 1);

            float _z = AngleInDegree(startPos, currentPos);

            img.transform.localRotation = Quaternion.Euler(0, 0, _z);

            //드래그중에 우클릭으로 멀티 타워 정보 저장하기
            SaveMultipleTowers();
        }

        //드래그 완료
        if (Input.GetMouseButtonUp(0))
        {
            //도착 위치
            arrive = Input.mousePosition;
            img.gameObject.SetActive(false);

            //팝업 체크
            Pop_UpgradePanel();
            //도착 타워 저장
            SaveTargetTower();

        }


        //유닛이 도착할 타워가 지정 됐을 때
        if (TowerManager.Instance.arriveTower != null)
        {
            // Debug.Log("타겟타워 지정 됐음");
            if (isMultiSelected) //타워가 멀티로 선택 되었으면?
            {
               // Debug.Log("멀티타워 : " + percentage);
                SendUnit(percentage);
                SendUnitFromMultipleTowers(percentage);

                //병력을 보내고 나면 myTower와 towardTower 컨테이너 비우고 multiselect ->false
                TowerManager.Instance.ResetBothTowers();
                TowerManager.Instance.departTowers.Clear();
                isMultiSelected = false;
            }
            else
            {
                SendUnit(percentage);

                //병력을 보내고 나면 myTower와 towardTower 컨테이너 비우기
                TowerManager.Instance.ResetBothTowers();
            }

        }

    }

    public void Pop_UpgradePanel()
    {

        //클릭 했을 때 마우스 위치와 클릭 버튼을 뗐을 때 마우스 위치 거리비교 
        //거의 제자리 클릭으로 판명나면 upgrade PopUp/ PopDown
        if (Vector3.Distance(startPos, Input.mousePosition) <= 0.2f)
        {
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hit;

            UpgradePanel _myPanel = transform.GetComponent<UpgradePanel>();

            if (Physics.Raycast(_ray, out _hit,100,mask))
            {
                if (_hit.transform.CompareTag("Tower") &&
                    _hit.transform.GetComponent<Building>().isPlayerTeam)
                {
                    if (!_myPanel.isPopUp)
                    {
                        _myPanel.PopUp(_hit);
                    }
                    else
                    {
                        _myPanel.PopDown();
                     //   Debug.Log("플레이어 타워");
                    }
                }
                else
                {
                   // Debug.Log("플레이어 타워 아님");
                    _myPanel.PopDown();
                }
            }

        }

    }


    public void SaveDepartTower()
    {
        //출발 위
        departure = startPos;
        img.transform.position = startPos;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (TowerManager.Instance.departTower != null)
        {
            TowerManager.Instance.ResetBothTowers();
        }

        if (Physics.Raycast(ray, out hit,100,mask))
        {
            if (hit.transform.CompareTag("Tower"))
            {
                Building _selectedTower = hit.transform.GetComponent<Building>();
                if (_selectedTower.isPlayerTeam)
                {
                  
                    TowerManager.Instance.SetDepartTower(hit);

                    circle = _selectedTower.effectCircle;
                    circle.GetComponent<TowerEffect>().ActiveCircle(5f, TowerManager.Instance.playerColor, _selectedTower.myId);
                    circle.SetActive(true);
                }
            }
        }
    }

    private void SaveMultipleTowers()
    {
        if (Input.GetMouseButtonDown(1))
        {

            rayR = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(rayR, out hitR, 100, mask) && hitR.transform.CompareTag("Tower"))
            {
                Building _selectedTower = hitR.transform.GetComponent<Building>();

                if (_selectedTower.isPlayerTeam)
                {
                    //list에 hitR에 저장된 gameobject가 없으면
                    if (!TowerManager.Instance.departTowers.Contains(hitR.transform.GetComponent<Building>()))
                    {
                        //list에 gameobject를 담아준다
                        TowerManager.Instance.departTowers.Add(hitR.transform.GetComponent<Building>());
                        isMultiSelected = true;
                        // Debug.Log("멀티타워 선택");

                        #region SelectedTower CircleEffect
                        circle = _selectedTower.effectCircle;
                        circle.GetComponent<TowerEffect>().ActiveCircle(5f, TowerManager.Instance.playerColor, _selectedTower.myId);
                        circle.SetActive(true);
                        #endregion
                    }
                }
            }
        }
    }

    private void SaveTargetTower()
    {
       
        if (TowerManager.Instance.departTower != null)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100, mask))
            {
                //Debug.Log(hit.transform.name);
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
            }

            if (TowerManager.Instance.arriveTower == null)
            {
                TowerManager.Instance.departTower = null;
                TowerManager.Instance.departTowers.Clear();
            }
        }
    }

    public void SendUnit(float percent)
    {
        //출발하는 타워에 저장된 병사의 수
        int _size = TowerManager.Instance.departTower.unit;

        // Debug.Log(percent);
        // (25%,50%,75%,100%) UI에서 세팅한 percentage에 맞춰 병력을 보내기 위한 용도
        _size = (int)(_size * percent);

        float[] value = { 0, 3, -3, 6, -6 };
        column = 5;

        Transform target = TowerManager.Instance.arriveTower.transform;

        Vector3 _dir = (target.position - TowerManager.Instance.GetDepartPos()).normalized;
        _dir.y = 0;
        //_size만큼의 병력을 미리 생성된 unit중 활성화하기
        for (int i = 0; i < _size; i++)
        {
            GameObject _unit = ObjectPool.instance.GetObjectFromPooler("Unit");
            if (_unit != null)
            {
                _unit.transform.position = TowerManager.Instance.GetDepartPos();
                //활성화시 방향을 타겟방향을 바라보고 , 그 방향에서 왼쪽 오른쪽으로 조금씩 각도를 더 틀어준다.
                //unit이 움직일 때 transform.forward 방향으로 가면서 퍼져나가는 모양을 보여줌
                _unit.transform.rotation = Quaternion.LookRotation(_dir, Vector3.up);
                _unit.transform.Rotate(Vector3.up * (value[i % column]));

                _unit.transform.GetComponent<UnitMove>().InitMushroom(target, 2, TowerManager.Instance.departTower.myTeam);
                _unit.SetActive(true);

                TowerManager.Instance.departTower.unitCount--;
                TowerManager.Instance.departTower.showUnit.text = TowerManager.Instance.departTower.unit.ToString();
            }
        }
    }


    public void SendUnitFromMultipleTowers(float percent)
    {
        int _towerSize = TowerManager.Instance.departTowers.Count;
        Transform _target = TowerManager.Instance.arriveTower.transform;
        float[] value = { 0, 3, -3, 6, -6 };
        column = 5;

        for (int i = 0; i < _towerSize; i++)
        {
            Vector3 _dir = (_target.position - TowerManager.Instance.departTowers[i].transform.position).normalized;

            int _unitSize = TowerManager.Instance.departTowers[i].unitCount;
            _unitSize = (int)(_unitSize * percent);

            for (int j = 0; j < _unitSize; j++)
            {
                GameObject _unit = ObjectPool.instance.GetObjectFromPooler("Unit");

                if (_unit != null)
                {
                    _unit.transform.position = TowerManager.Instance.departTowers[i].transform.position;
                    //활성화시 방향을 타겟방향을 바라보고 , 그 방향에서 왼쪽 오른쪽으로 조금씩 각도를 더 틀어준다.
                    //unit이 움직일 때 transform.forward 방향으로 가면서 퍼져나가는 모양을 보여줌
                    _unit.transform.rotation = Quaternion.LookRotation(_dir, Vector3.up);
                    _unit.transform.Rotate(Vector3.up * (value[j % column]));

                    _unit.transform.GetComponent<UnitMove>().InitMushroom(_target, 2, TowerManager.Instance.departTowers[i].myTeam);
                    _unit.SetActive(true);

                    TowerManager.Instance.departTowers[i].unitCount--;
                    TowerManager.Instance.departTowers[i].showUnit.text = TowerManager.Instance.departTowers[i].unit.ToString();
                }
            }

        }
    }

    //public void ReactCircle(Vector3 circleScale, float speed)
    //{
    //    circle.transform.localScale = Vector3.Lerp(circle.transform.localScale,
    //           circleScale, Time.deltaTime * speed);
    //}
    //public void DeActiveReactCircle(float speed)
    //{
    //    circle.transform.localScale = Vector3.Lerp(circle.transform.localScale,
    //            Vector3.zero, Time.deltaTime * speed);
    //}
    IEnumerator DevideUnitRow()
    {
       
        yield return null;
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


    public float SendPercentage
    {
        get { return percentage; }
        set { percentage = value; }
    }


}
