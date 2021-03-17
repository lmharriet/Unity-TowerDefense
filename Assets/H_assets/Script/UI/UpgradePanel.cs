using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    public GameObject upgradePanel;
    public bool isPopUp;
    RaycastHit hitInfo;
    Building currentBuilding;
    public void PopUp(RaycastHit hit)
    {
        PopDown();
        isPopUp = true;
        hitInfo = hit;

        upgradePanel.transform.position = hitInfo.transform.position + new Vector3(0, 2.5f, 0);
        upgradePanel.SetActive(true);
    }

    public void PopDown()
    {
        isPopUp = false;

        if (upgradePanel.activeSelf)
        {
            upgradePanel.SetActive(false);
        }
    }



}

