using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    public GameObject[] upgradePanel = new GameObject[3];
    RaycastHit hitInfo;

    public void PopUp(RaycastHit hit)
    {
        hitInfo = hit;
        EnumSpace.TOWERKIND _kind = hit.transform.GetComponent<Building>().kind;
        if (_kind == EnumSpace.TOWERKIND.TOWN)
        {
            upgradePanel[0].transform.position = hitInfo.transform.position;
            upgradePanel[0].SetActive(true);
        }
        else if (_kind == EnumSpace.TOWERKIND.DEFENSE)
        {
            upgradePanel[1].transform.position = hitInfo.transform.position;
            upgradePanel[1].SetActive(true);
        }
        else if (_kind == EnumSpace.TOWERKIND.FACTORY)
        {
            upgradePanel[2].transform.position = hitInfo.transform.position;
            upgradePanel[2].SetActive(true);
        }

    }

    public void PopDown()
    {
        for (int i = 0; i < 3; i++)
        {
            if (upgradePanel[i].activeSelf)
                upgradePanel[i].SetActive(false);
        }
    }

    


}
