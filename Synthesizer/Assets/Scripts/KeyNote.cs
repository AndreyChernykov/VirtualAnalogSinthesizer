using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyNote : MonoBehaviour
{
    [SerializeField] Slider sliderDist;//слайдер дисторшена
    [SerializeField] TextMeshProUGUI textDist;
    [SerializeField] Slider sliderDelay;
    [SerializeField] TextMeshProUGUI textDelay;
    [SerializeField] Slider sliderDecay;
    [SerializeField] TextMeshProUGUI textDecay;
    private Filters filters;//скрипт фильтров
    PlayNote playNote;

    public void Start()
    {
        playNote = gameObject.GetComponent<PlayNote>();

        filters = GameObject.Find("OsciliatorSinus").GetComponent<Filters>();

    }

    public void ClickKeyNote(string nameKey)//обрабатываем нажатие клавиши с нотой и получаем ее название
    {        
        playNote.PlayToNote(nameKey);

        SliderDist();
        SliderDelay();
        SliderDecay();
    }

    public void NoClickNote()//при отпускании клавиши с нотой
    {
        playNote.StopPlayNote();
    }

    public void SliderDist()//дисторшен
    {
        filters.DistValue = sliderDist.value;
        textDist.text = "Dist " + (sliderDist.value * 100).ToString("0");
        filters.Distortion();//дисторшен фильтр
    }


    public void SliderDelay()
    {
        textDelay.text = "Delay " + sliderDelay.value.ToString("0");
        filters.Delay = sliderDelay.value;
    }

    public void SliderDecay()
    {
        filters.Decay = sliderDecay.value;
        textDecay.text = "Decay " + (sliderDecay.value * 100).ToString("0");
    }
}
