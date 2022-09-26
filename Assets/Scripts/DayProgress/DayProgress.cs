using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayProgress : MonoBehaviour
{
    public static DayProgress instance;

    public delegate void DayChanged(int day);
    public event DayChanged OnDayChanged;

    public delegate void TimeChanged(int time);
    public event TimeChanged OnTimeChanged;

    public int day;
    public int time;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(ProgressDay());
    }

    private IEnumerator ProgressDay()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            time++;
            if (time >= 24)
            {
                time = 0;
                day++;
                OnDayChanged?.Invoke(day);
            }
            OnTimeChanged?.Invoke(time);
        }
    }

    
}
