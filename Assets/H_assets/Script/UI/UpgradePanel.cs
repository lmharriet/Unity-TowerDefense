using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    public enum UPGRADE
    {
        LEVEL_UP, TO_TOWER, TO_FACTORY
    }

    public UPGRADE upgrade;
    public GameObject[] upgradePanel = new GameObject[3];
    public bool isPopUp;
    RaycastHit hitInfo;
    Building currentBuilding;

    public void PopUp(RaycastHit hit)
    {
        isPopUp = true;
        hitInfo = hit;
        currentBuilding = hit.transform.GetComponent<Building>();
        EnumSpace.TOWERKIND _kind = hit.transform.GetComponent<Building>().kind;
        if (_kind == EnumSpace.TOWERKIND.TOWN)
        {
            upgradePanel[0].transform.position = hitInfo.transform.position + new Vector3(0, 2.5f, 0);
            upgradePanel[0].SetActive(true);
        }
        else if (_kind == EnumSpace.TOWERKIND.DEFENSE)
        {
            upgradePanel[1].transform.position = hitInfo.transform.position + new Vector3(0, 2.5f, 0);
            upgradePanel[1].SetActive(true);
        }
        else if (_kind == EnumSpace.TOWERKIND.FACTORY)
        {
            upgradePanel[2].transform.position = hitInfo.transform.position + new Vector3(0, 2.5f, 0);
            upgradePanel[2].SetActive(true);
        }

    }

    public void PopDown()
    {
        isPopUp = false;
        for (int i = 0; i < 3; i++)
        {
            if (upgradePanel[i].activeSelf)
            {
                upgradePanel[i].SetActive(false);

            }
        }
        currentBuilding = null;
    }

    public void LevelUp()
    {
        if (upgrade == UPGRADE.LEVEL_UP)
        {
            currentBuilding.TowerLever++;
            currentBuilding.unitCount -= currentBuilding.upgradeCost;

        }
    }

    public DefenseTower ChangeToTower()
    {

        if (upgrade == UPGRADE.TO_TOWER)
        {
            //int Id, int units, int myLevel, bool isPlayer, EnumSpace.TEAMCOLOR col

            DefenseTower _tower = new DefenseTower(currentBuilding.myId, currentBuilding.unitCount, currentBuilding.TowerLever,
                                                    currentBuilding.isPlayerTeam, currentBuilding.myColor);

            return _tower;
        }

        return null;

    }

    public FactoryTower ChangeToFactory()
    {
        if (upgrade == UPGRADE.TO_FACTORY)
        {
            FactoryTower _factory = new FactoryTower(currentBuilding.myId, currentBuilding.unitCount, currentBuilding.TowerLever,
                                                    currentBuilding.isPlayerTeam, currentBuilding.myColor);

            return _factory;
        }
        return null;
    }


}

