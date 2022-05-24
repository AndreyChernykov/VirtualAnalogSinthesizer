using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LFO : MonoBehaviour
{
    private float amplitude = -0.03f;
    float timeOscillation = 0.5f;
    private float hz = 0.5f;
    private bool onLFO = false;//включено ли LFO
    
    private AudioSource audioSource;
    

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();

        
    }

    public void StartLFO()
    {
        StartCoroutine(OscillatorSquare());
    }

    private IEnumerator OscillatorSquare()//лфо квадрат
    {
        if (!onLFO) StopCoroutine(OscillatorSquare());
        while (onLFO)
        {
            audioSource.pitch -= amplitude;
            yield return new WaitForSeconds(timeOscillation);
            audioSource.pitch += amplitude;
            yield return new WaitForSeconds(timeOscillation);
        }
    }

    public bool OnLFO
    {
        get { return onLFO; }
        set { onLFO = value; }
    }

    public float TimeOscillation
    {
        get { return hz/timeOscillation; }
        set { timeOscillation = hz/value; }
    }

    public float Amplitude
    {
        get { return amplitude; }
        set { amplitude = value; }
    }

}
