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
        currentBuilding = hit.transform.GetComponent<Building>();
        upgradePanel.transform.position = hitInfo.transform.position + new Vector3(-0.5f, 4f, -2f);
        upgradePanel.SetActive(true);

        upgradePanel.transform.GetChild(0).GetComponent<UpgradeType>().SetCurrentBuilding(currentBuilding);
       // upgradePanel.transform.GetChild(1).GetComponent<UpgradeType>().SetCurrentBuilding(currentBuilding);


    }

    public void PopDown()
    {
        isPopUp = false;

        if (upgradePanel.activeSelf)
        {
            currentBuilding = null;

            upgradePanel.transform.GetChild(0).GetComponent<UpgradeType>().SetCurrentBuilding(currentBuilding);
           // upgradePanel.transform.GetChild(1).GetComponent<UpgradeType>().SetCurrentBuilding(currentBuilding);
            upgradePanel.SetActive(false);
        }
    }

}

