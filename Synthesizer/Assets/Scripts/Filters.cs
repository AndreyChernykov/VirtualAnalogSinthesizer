using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filters : MonoBehaviour
{
    
    private AudioDistortionFilter distFilter;
    private AudioEchoFilter echoFilter;
    private AudioLowPassFilter lowPassFilter;
    private float distValue;//уровень дисторшена

    public void Start()
    {
        distFilter = gameObject.GetComponent<AudioDistortionFilter>();
        echoFilter = gameObject.GetComponent<AudioEchoFilter>();
        lowPassFilter = gameObject.GetComponent<AudioLowPassFilter>();
    }

    public void Distortion()
    {
        distFilter.distortionLevel = distValue;
    }

    public float DistValue
    {
        set { distValue = value; }
    }

    public float Delay//делей
    {
        set { echoFilter.delay = value; }
    }

    public float Decay//декей
    {
        set { echoFilter.decayRatio = value; }
    }

    public float Cutoff
    {
        get { return lowPassFilter.cutoffFrequency; }
        set { lowPassFilter.cutoffFrequency = value; }
    }

    public float Resonance
    {
        get { return lowPassFilter.lowpassResonanceQ; }
        set { lowPassFilter.lowpassResonanceQ = value; }
    }
}
