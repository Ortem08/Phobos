using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSoundScript : MonoBehaviour
{
    public AudioClip[] Steps;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.volume = 0.1f;
    }
    
    public void PlayStep()
    {
        source.PlayOneShot(Steps[Random.Range(0, Steps.Length)]);
    }
}
