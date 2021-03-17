using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBomb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
           //Debug.Log("유닛");
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Floor"))
        {
            //Debug.Log("땅");
            gameObject.SetActive(false);
        }
    }
}
