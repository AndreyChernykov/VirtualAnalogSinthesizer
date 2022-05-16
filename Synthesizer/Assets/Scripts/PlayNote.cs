using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayNote : MonoBehaviour
{
    private KeyOctav keyOctav;//номер выбраной октавы
    private FrequencyNotes frqNote;//частота ноты
    private AudioSource oscilatorSinus;//осцилятор синусойды

    public void Start()
    {
        keyOctav = gameObject.GetComponent<KeyOctav>();
        oscilatorSinus = GameObject.Find("OsciliatorSinus").GetComponent<AudioSource>();


        frqNote = new FrequencyNotes();
    }

    public void PlayToNote(string nameKey)//проигрываем ноту
    {
        oscilatorSinus.volume = 1;
        Note note = new Note(nameKey, keyOctav.NumOctav, frqNote.GetFrequency(nameKey));
        
        oscilatorSinus.pitch = note.FrqNote;//устанавливаем высоту ноты

        oscilatorSinus.Play();
        Debug.Log(note.NoteName);

    }

    public void StopPlayNote()//останавливаем звучание ноты
    {
        
        StartCoroutine(StopedPlay());
    }

    

    IEnumerator StopedPlay()
    {
        yield return new WaitForSeconds(0.1f);
        oscilatorSinus.volume = 0;
        yield return new WaitForSeconds(0.1f);
        oscilatorSinus.Stop();
        StopCoroutine(StopedPlay());
        
    }
}
