using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private PlayerStatistics stats;

    [SerializeField]
    private UnityEvent deathEvent;

    private AudioSource aud;

    void Start()
    {
        stats.Visibility = 1;
        stats.isCaught = false;
    }

    void Update()
    {
        if (stats.isCaught == true)
            deathEvent.Invoke();
    }
}
