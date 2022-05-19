using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer : MonoBehaviour
{
    private AudioSource oscilatorSinus;//осцилятор синусойды
    private AudioSource oscillatorSaw;//осциллятор пилы

    private float speed = 0.5f;//скорость переключения нот
    private int seqLength = 16;//количество шагов секвенсора
    private bool toPlaySeq = false;
    private KeyNote keyNote;
    Note note;
    private Note[] noteArr;//массив нот секвенции

    public int SeqLength
    {
        get { return seqLength; }
    }


    public void SetNote(Note note)//запись в массив секвенсора ноты
    {
       if(keyNote.NumKeySeqToClick < seqLength) noteArr[keyNote.NumKeySeqToClick] = note;
    }

    public void Start()
    {
        noteArr = new Note[seqLength];
        for (int i = 0; i < noteArr.Length; i++) noteArr[i] = new Note("C", 0);

        keyNote = GameObject.Find("Canvas").GetComponent<KeyNote>();
        oscilatorSinus = GameObject.Find("OsciliatorSinus").GetComponent<AudioSource>();
        oscillatorSaw = GameObject.Find("OsciliatorSaw").GetComponent<AudioSource>();

    }

    public void Play()
    {
        
        if (!toPlaySeq)
        {

            toPlaySeq = true;
            StartCoroutine(PlaySequence());
            
        }

    }

    public void Stop()
    {
        toPlaySeq = false;
        StopCoroutine(PlaySequence());
        oscilatorSinus.volume = 0;
        oscillatorSaw.volume = 0;
    }

    private IEnumerator PlaySequence()
    {      
        while (toPlaySeq)
        {
            for (int i = 0; i < noteArr.Length && toPlaySeq; i++)
            {
                oscilatorSinus.volume = keyNote.OnSinOsc ? 1 : 0;
                oscillatorSaw.volume = keyNote.OnSawOsc ? 1 : 0;
                //oscilatorSinus.volume = 1;
                note = noteArr[i];
                oscilatorSinus.pitch = note.FrqNote;
                oscillatorSaw.pitch = note.FrqNote;
                keyNote.KeysSeqMagic(i);
                Debug.Log(noteArr[i].NoteName);
                yield return new WaitForSeconds(speed);
            }
        }
    }

    public float BPM//устанавливаем скорость секвенсора
    {
        set { speed = 60 / value; }
    }
}
