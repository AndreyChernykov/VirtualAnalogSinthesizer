using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LFO : MonoBehaviour
{
    private float min = -0.03f;//максимальный уровень
    private float max = 0.03f;//минимальный уровень
    public float timeOscillation = 0.5f;
    private string onLFO;//какое включено LFO
    private AudioSource audioSource;


    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    public void OnLFO(string s)
    {
        onLFO = s;

        switch (s)
        {
            case "square":
                StartCoroutine(OscillatorSquare());
                break;
            case "triangle":
                StartCoroutine(OscillatorTriangle());
                break;
        }
    }

    private IEnumerator OscillatorSquare()//лфо квадрат
    {
        if (onLFO != "square") StopCoroutine(OscillatorSquare());
        while (onLFO.Equals("square"))
        {
            audioSource.pitch += min;
            yield return new WaitForSeconds(timeOscillation);
            audioSource.pitch += max;
            yield return new WaitForSeconds(timeOscillation);
        }
    }


    private IEnumerator OscillatorTriangle()//лфо треугольник
    {
        Debug.Log("triangle");
        while (audioSource.pitch > audioSource.pitch + max)
        {
            audioSource.pitch += 0.001f;
            yield return new WaitForSeconds(timeOscillation);
        }
        while (audioSource.pitch < audioSource.pitch - min)
        {
            audioSource.pitch -= 0.001f;
            yield return new WaitForSeconds(timeOscillation);
        }


    }
}
