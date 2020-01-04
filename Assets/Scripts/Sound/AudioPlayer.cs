using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource myAS;

    public AudioClip[] audioClips;

    public void PlayOnceRandomPitch(AudioClip audioClip)
    {
        myAS.pitch = Random.Range(0.8f, 1.2f);
        myAS.PlayOneShot(audioClip);
    }

    public void PlayOnce(AudioClip audioClip)
    {
        myAS.PlayOneShot(audioClip);
    }

    public void PlayAudioSourceLoop()
    {
        myAS.mute = false;
    }

    public void StopAudioSourceLoop()
    {
        myAS.mute = true;
    }

    private void Awake()
    {
        myAS = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
