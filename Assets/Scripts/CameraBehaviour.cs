using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraBehaviour : MonoBehaviour
{
    public float shakeDuration; //Time the sahke 
    public float shakeAmplitude; //Cinemachine noise profiler parameter
    public float shakeFrequency; //Cinemachine noise profiler parameter

    public float shakeElapsedTime;

    public CinemachineVirtualCamera actualCamera;
    public CinemachineVirtualCamera CM_FollowPlayer;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    public void ActivateFollowCam()
    {
        actualCamera = CM_FollowPlayer;
        CM_FollowPlayer.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {

        virtualCameraNoise = actualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update Shake Timer
        if (shakeElapsedTime > 0)
        {
            shakeElapsedTime -= Time.deltaTime;
        }

        if (actualCamera != null && virtualCameraNoise != null)
        {
            if (shakeElapsedTime > 0)
            {
                // Set Cinemachine Camera Noise parameters
                virtualCameraNoise.m_AmplitudeGain = shakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = shakeFrequency;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
                virtualCameraNoise.m_AmplitudeGain = 0f;
                shakeElapsedTime = 0f;
            }
        }
    }

    public void DoCameraShake()
    {

        if (actualCamera != null)
        {
            shakeDuration = .2f;
            virtualCameraNoise = actualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }
        shakeElapsedTime = shakeDuration;
    }

    public void DoLongCameraShake()
    {

        if (actualCamera != null)
        {
            shakeDuration = 3f;
            virtualCameraNoise = actualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }
        shakeElapsedTime = shakeDuration;
    }
}
