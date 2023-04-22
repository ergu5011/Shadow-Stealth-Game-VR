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
        // Loweres players detection to minimum when entering high darkness
        if (other.gameObject.CompareTag("Player") && isInnerTrigger == true)
        {
            stats.Visibility = 0f;
        }     
        // Loweres players detection to half when entering medium darkness
        else if (other.gameObject.CompareTag("Player"))
        {
            stats.Visibility = 0.5f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Raises players detection when transitioning from high darkness to medium darkness
        if (other.gameObject.CompareTag("Player") && isInnerTrigger == true)
        {
            stats.Visibility = 0.5f;
        }
        // Raises players detection to max when fully exiting shadow
        else if (other.gameObject.CompareTag("Player"))
        {
            stats.Visibility = 1f;
        }
    }
}
