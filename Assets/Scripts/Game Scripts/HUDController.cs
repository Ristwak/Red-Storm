using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [Header("UI References")]
    public Text messageText;   // For showing mission messages
    public Text timerText;     // Optional - countdown timer

    private float missionTime = 0f;
    private bool timerRunning = false;

    void Update()
    {
        if (timerRunning)
        {
            missionTime -= Time.deltaTime;
            if (missionTime < 0) missionTime = 0;

            if (timerText != null)
                timerText.text = "Time: " + Mathf.CeilToInt(missionTime).ToString();
        }
    }

    public void ShowMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
        }
        Debug.Log("HUD Message: " + message); // for testing
    }

    public void StartTimer(float time)
    {
        missionTime = time;
        timerRunning = true;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }
}
