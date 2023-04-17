using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    public PlayerStatistics stats;

    void Start()
    {
        
    }

    void Update()
    {
        Debug.Log(stats.Visibility);
    }
}
