using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyNote : MonoBehaviour
{
    [SerializeField] Slider sliderDist;//слайдер дисторшена
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
    }

    public void NoClickNote()//при отпускании клавиши с нотой
    {
        playNote.StopPlayNote();
    }

    public void SliderDist()//дисторшен
    {
        filters.DistValue = sliderDist.value;
        filters.Distortion();//дисторшен фильтр
    }
}
