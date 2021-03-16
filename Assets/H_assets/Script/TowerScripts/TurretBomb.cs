using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBomb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Floor"))
        {
            gameObject.SetActive(false);
        }
    }
}
