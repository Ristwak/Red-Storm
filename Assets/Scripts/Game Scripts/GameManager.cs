using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton for easy access

    [Header("Mission Settings")]
    public Transform[] dataPoints;   // 3 data point locations
    public Transform baseStation;    // base position
    private int currentPointIndex = 0;

    [Header("References")]
    public CompassController compass;    // will control the compass arrow
    public StormController storm;        // will control the storm
    public HUDController hud;            // for timer, text etc.

    private bool missionStarted = false;
    private bool missionComplete = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        StartMission();
    }

    void StartMission()
    {
        missionStarted = true;
        hud.ShowMessage("Welcome Cadet! Collect all data points before the storm gets too strong!");
        compass.SetTarget(dataPoints[currentPointIndex]);
        storm.BeginStorm(); // start storm buildup
    }

    public void CollectDataPoint()
    {
        currentPointIndex++;

        if (currentPointIndex < dataPoints.Length)
        {
            hud.ShowMessage("Good job! Head to the next point.");
            compass.SetTarget(dataPoints[currentPointIndex]);
        }
        else
        {
            hud.ShowMessage("Storm is strong! Return to base!");
            compass.SetTarget(baseStation);
        }
    }

    public void ReachBase()
    {
        if (currentPointIndex >= dataPoints.Length && !missionComplete)
        {
            missionComplete = true;
            hud.ShowMessage("Mission Complete! You survived the Red Storm!");
            storm.EndStorm(); // fade storm
        }
    }
}
