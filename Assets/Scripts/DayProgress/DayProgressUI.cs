using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayProgressUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dayText;
    [SerializeField] private TextMeshProUGUI _timeText;

    DayProgress _dayProgress;

    private void Start()
    {
        _dayProgress = DayProgress.instance;
        _dayProgress.OnDayChanged += UpdateDay;
        _dayProgress.OnTimeChanged += UpdateTime;

        UpdateDay(_dayProgress.day);
        UpdateTime(_dayProgress.time);
    }

    private void UpdateDay(int day)
    {
        _dayText.text = day.ToString() + " Day";
    }

    private void UpdateTime(int time)
    {
        _timeText.text = time.ToString() + ":00";
    }
    
}
