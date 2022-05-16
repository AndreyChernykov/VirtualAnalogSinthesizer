using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filters : MonoBehaviour
{
    
    private AudioDistortionFilter distFilter;
    private AudioEchoFilter echoFilter;
    private float distValue;//уровень дисторшена

    public void Start()
    {
        distFilter = gameObject.GetComponent<AudioDistortionFilter>();
        echoFilter = gameObject.GetComponent<AudioEchoFilter>();
        
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
}
