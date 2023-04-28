using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DarknessMeter : MonoBehaviour
{
    [SerializeField]
    private PlayerStatistics stats;

    [SerializeField]
    private TMP_Text darknessText;

    private float current;

    void Update()
    {
        if (stats.Visibility == 1)
            current += 10f * Time.deltaTime;
        else if (stats.Visibility == 0)
            current -= 10f * Time.deltaTime;
        else if (stats.Visibility == 0.5 && current < 50)
            current += 10f * Time.deltaTime;
        else if (stats.Visibility == 0.5 && current > 50)
            current -= 10f * Time.deltaTime;

        darknessText.text = "Vis: " + current;

        current = Mathf.Clamp(current, 0, 100);
    }
}
