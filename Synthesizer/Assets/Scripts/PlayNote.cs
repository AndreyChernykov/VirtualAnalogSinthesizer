using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayNote : MonoBehaviour
{
    private KeyOctav keyOctav;//номер выбраной октавы
    private GameObject oscillatorSin;//осцилятор синусойды
    private GameObject oscillatorSaw;//осциллятор пилы
    private AudioSource oscilatorSinus;
    private AudioSource oscillatorAudioSaw;
    private Sequencer sequencer;
    private KeyNote keyNote;


    public void Start()
    {
        keyOctav = gameObject.GetComponent<KeyOctav>();
        oscillatorSin = GameObject.Find("OsciliatorSinus");
        oscillatorSaw = GameObject.Find("OsciliatorSaw");
        oscilatorSinus = oscillatorSin.GetComponent<AudioSource>();
        oscillatorAudioSaw = oscillatorSaw.GetComponent<AudioSource>();
        sequencer = GameObject.Find("Canvas").GetComponent<Sequencer>();
        keyNote = gameObject.GetComponent<KeyNote>();

        oscilatorSinus.Play();
        oscillatorAudioSaw.Play();

    }

    public void PlayToNote(string nameKey)//проигрываем ноту
    {
        Note note = new Note(nameKey, keyOctav.NumOctav);
        sequencer.SetNote(note);
        if (keyNote.OnSinOsc)
        {
            oscilatorSinus.volume = 1;           
            oscilatorSinus.pitch = note.FrqNote;//устанавливаем высоту ноты  
        }
        if (keyNote.OnSawOsc)
        {
            oscillatorAudioSaw.volume = 1;
            oscillatorAudioSaw.pitch = note.FrqNote;//устанавливаем высоту ноты  
        }

        Debug.Log(note.NoteName);
    }

    public void StopPlayNote()//останавливаем звучание ноты
    {
        oscilatorSinus.volume = 0;
        oscillatorAudioSaw.volume = 0;
    }

    


}
