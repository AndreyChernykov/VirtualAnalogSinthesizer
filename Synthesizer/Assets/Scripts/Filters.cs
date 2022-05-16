using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filters : MonoBehaviour
{
    
    private AudioDistortionFilter distFilter;
    private float distValue;//уровень дисторшена

    public void Start()
    {
        distFilter = gameObject.GetComponent<AudioDistortionFilter>();
        
    }

    public void Distortion()
    {
        distFilter.distortionLevel = distValue;
    }

    public float DistValue
    {
        set { distValue = value; }
    }
}
