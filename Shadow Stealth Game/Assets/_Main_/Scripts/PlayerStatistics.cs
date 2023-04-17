using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Statistics")]
public class PlayerStatistics : ScriptableObject
{
    public float Visibility;

    public void HighVisibility()
    {
        Visibility = 10;
    }
    
    public void MediumVisibility()
    {
        Visibility = 5;
    }

    public void LowVisibility()
    {
        Visibility = 0;
    } 
}