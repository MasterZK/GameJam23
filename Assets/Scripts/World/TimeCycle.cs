using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum DayNightCycle
{
    day = 1,
    night = 0
}

public struct TimeFormat
{
    public int Hours;
    public int Minutes;

    public bool AM_PM; //false = AM , true = PM
    public DayNightCycle CurrentState;

    private int hour24;

    public void SetMorning()
    {
        Hours = 8;
        Minutes = 0;

        AM_PM = false;
        CurrentState = DayNightCycle.day;
    }

    public void SetEventing()
    {
        SetMorning();

        AM_PM = true;
        CurrentState = DayNightCycle.night;
    }   
    
    public void CheckDayNightState()
    {
        hour24 = AM_PM ? Hours + 12 : Hours;

        CurrentState = DayNightCycle.night;

        if (hour24 > 8 && hour24 < 20)
            CurrentState = DayNightCycle.day;
    }

    public static bool operator ==(TimeFormat time, string timeTwo)
    {
        return time.ToString() == timeTwo;
    }

    public static bool operator !=(TimeFormat time, string timeTwo)
    {
        return !time.ToString().Equals(timeTwo);
    }

    public override string ToString()
    {
        var am_PM = AM_PM ? "PM" : "AM";
        return Hours.ToString("D2") + ":" + Minutes.ToString("D2") + " " + am_PM;
    }

}

public class TimeCycle : MonoBehaviour
{
    [Tooltip("Time per 30 min ingame in sec irl | default: 2.5f equats to 60 sec for 12 Hours")]
    [SerializeField] private float minuteDuration = 2.5f;
    [SerializeField] private TextMeshProUGUI timeUI;
    [SerializeField] private TextMeshProUGUI dayUI;

    public TimeFormat CurrentTime;

    public bool startTime = false;
    private float timeLastCycle;

    private void Start()
    {
        CurrentTime.SetMorning();
    }

    private void FixedUpdate()
    {
        deltaToTime();
        CurrentTime.CheckDayNightState();

        printTime();
        printDayUI();
    }

    private void deltaToTime()
    {
        if (!startTime)
            return;

        timeLastCycle += Time.deltaTime;

        if (timeLastCycle < minuteDuration)
            return;

        timeLastCycle = 0;
        CurrentTime.Minutes += 30;


        if (CurrentTime.Minutes == 60)
        {
            CurrentTime.Hours++;
            CurrentTime.Minutes = 0;
        }

        if (CurrentTime.Hours == 12)
        {
            CurrentTime.Hours = 0;
            CurrentTime.AM_PM = !CurrentTime.AM_PM;
        }

    }

    public DayNightCycle GetDayNightState()
    {
        return CurrentTime.CurrentState;
    }

    private void printTime()
    {
        if (timeUI == null)
            return;

        timeUI.text = CurrentTime.ToString();
    }

    private void printDayUI()
    {
        if (dayUI.text == CurrentTime.CurrentState.ToString())
            return;

        dayUI.text = CurrentTime.CurrentState.ToString();
    }
}
