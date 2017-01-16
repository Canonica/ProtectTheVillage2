using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField]
    float DayTime;

    float timer;

    void Start()
    {
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float ratio = timer / DayTime;
        if (ratio >= 1)
        {
            WavesManager.Night();
        }
        else if (ratio >= 2)
        {
            WavesManager.newDay();
            timer = 0;
        }
        transform.eulerAngles = new Vector3(ratio * 180, 0, 0);
    }
}