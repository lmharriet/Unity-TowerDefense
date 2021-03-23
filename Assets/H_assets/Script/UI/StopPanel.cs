using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPanel : MonoBehaviour
{
    [SerializeField]
    GameObject stopPopUp;

    //int touchCount;
  
    public void ActivePopUp()
    {
        stopPopUp.SetActive(true);
        //touchCount = 1;
        Time.timeScale = 0;
    }
    public void DeActivePopUp()
    {
        stopPopUp.SetActive(false);
       //touchCount = 0;
        Time.timeScale = 1;
    }
}