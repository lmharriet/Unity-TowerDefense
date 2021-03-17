using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeType : MonoBehaviour
{
    public EnumSpace.UPGRADE upgrade;

    public Building currentBuilding;

    private void OnEnable()
    {
        RaycastHit _hit;

        if (Physics.Raycast(transform.position, (Vector3.down*2 + Vector3.forward*2), out _hit, 10f))
        {
            currentBuilding = _hit.transform.GetComponent<Building>();
            //Debug.Log(currentBuilding.gameObject.name);
            //if(_hit.transform.CompareTag("Tower"))
            //{
            //}
        }
    }
    private void OnMouseOver()
    {
       // Debug.Log("upgrade open");

        if (Input.GetMouseButtonUp(0))
        {
            switch (upgrade)
            {
                case EnumSpace.UPGRADE.LEVEL_UP:
                    LevelUp();

                    break;
                case EnumSpace.UPGRADE.TO_TOWER:

                    break;
                case EnumSpace.UPGRADE.TO_FACTORY:

                    break;
            }
        }

    }

    public void LevelUp()
    {
        if (upgrade == EnumSpace.UPGRADE.LEVEL_UP)
        {
            if (currentBuilding.unitCount - currentBuilding.Cost >= 0)
            {
                currentBuilding.unitCount -= currentBuilding.upgradeCost;
                currentBuilding.TowerLever++;
            }
            else
            {
                Debug.Log("유닛 부족으로 레벨업 실패");
            }

        }
    }

    public void SetCurrentBuilding(Building myBuilding)
    {
        currentBuilding = myBuilding;
    }
}



