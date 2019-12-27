using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    private float timeToStopVibration;
    public float startTimeToStopVibration;

    public float RVib;
    public float LVib;
    public void Vibrate(float RVibNew, float LVibNew, float timeNew)
    {
        RVib = RVibNew;
        LVib = LVibNew;
        startTimeToStopVibration = timeNew;
        timeToStopVibration = startTimeToStopVibration;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timeToStopVibration > 0)
        {
            hinput.gamepad[0].VibrateAdvanced(RVib, LVib);
            timeToStopVibration -= Time.deltaTime;
        }
        else
        {
            hinput.gamepad[0].StopVibration();
        }
    }
}
