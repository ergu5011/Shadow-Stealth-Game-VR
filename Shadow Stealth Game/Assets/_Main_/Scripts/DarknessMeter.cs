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

    private float target;
    private float current;
    private string toText;

    private void Start()
    {
        current = 50;
    }

    void Update()
    {
        target = stats.Visibility * 100;

        if (current == target)
            current = target;
        else if (current == target && target == 50)
            current = 50f;
        else if (current < target)
            current += 50f * Time.deltaTime;
        else
            current -= 50f * Time.deltaTime;

        darknessText.text = "Vis: " + current.ToString("F0") + " %";

        current = Mathf.Clamp(current, 0, 100);
    }
}
