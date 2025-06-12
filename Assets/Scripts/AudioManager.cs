using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioMixer mainMixer;
    [SerializeField] float masterVolumeValue = 10.0F; // between 0 and 10

    void Start()
    {
        
    }

    void Update()
    {
        // todo add all audio to mixer and set here, add slider in options menu for each
        mainMixer.SetFloat("MasterVolume", -(masterVolumeValue * 8.0F));
    }
}
