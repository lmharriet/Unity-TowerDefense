using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    public GameObject[] upgradePanel = new GameObject[3];
    public bool isPopUp;
    RaycastHit hitInfo;
    Building currentBuilding;
    public void PopUp(RaycastHit hit)
    {
        PopDown();
        isPopUp = true;
        hitInfo = hit;
        currentBuilding = hit.transform.GetComponent<Building>();
        EnumSpace.TOWERKIND _kind = currentBuilding.kind;
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
        // upgrade.SetCurrentBuilding(null);
    }



}

