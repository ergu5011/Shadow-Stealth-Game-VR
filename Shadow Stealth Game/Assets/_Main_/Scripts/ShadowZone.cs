using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowZone : MonoBehaviour
{
    [SerializeField]
    private PlayerStatistics stats;

    [SerializeField]
    private bool isInnerTrigger;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isInnerTrigger == true)
        {
            stats.LowVisibility();
        }     
        else if (other.gameObject.CompareTag("Player"))
        {
            stats.MediumVisibility();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isInnerTrigger == true)
        {
            stats.MediumVisibility();
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            stats.HighVisibility();
        }
    }
}
