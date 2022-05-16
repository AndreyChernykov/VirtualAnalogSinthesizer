﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayNote : MonoBehaviour
{
    private KeyOctav keyOctav;//номер выбраной октавы
    private FrequencyNotes frqNote;//частота ноты
    private GameObject oscillator;
    private AudioSource oscilatorSinus;//осцилятор синусойды
    


    public void Start()
    {
        keyOctav = gameObject.GetComponent<KeyOctav>();
        oscillator = GameObject.Find("OsciliatorSinus");
        oscilatorSinus = oscillator.GetComponent<AudioSource>();

        oscilatorSinus.Play();

        frqNote = new FrequencyNotes();
    }

    public void PlayToNote(string nameKey)//проигрываем ноту
    {
        oscilatorSinus.volume = 1;
        Note note = new Note(nameKey, keyOctav.NumOctav, frqNote.GetFrequency(nameKey));       
        oscilatorSinus.pitch = note.FrqNote;//устанавливаем высоту ноты
        
        //oscilatorSinus.Play();
        Debug.Log(note.NoteName);

    }

    public void StopPlayNote()//останавливаем звучание ноты
    {
        oscilatorSinus.volume = 0;
    }

    


}
