using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer : MonoBehaviour
{
    private AudioSource oscilatorSinus;//осцилятор синусойды
    private float speed = 1;//скорость переключения нот
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
        noteArr[keyNote.NumKeySeqToClick] = note;
    }

    public void Start()
    {
        noteArr = new Note[seqLength];
        for (int i = 0; i < noteArr.Length; i++) noteArr[i] = new Note("C", 0);

        keyNote = GameObject.Find("Canvas").GetComponent<KeyNote>();
        oscilatorSinus = GameObject.Find("OsciliatorSinus").GetComponent<AudioSource>();

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

    }

    private IEnumerator PlaySequence()
    {
        
        while (toPlaySeq)
        {
            for (int i = 0; i < noteArr.Length && toPlaySeq; i++)
            {
                oscilatorSinus.volume = 1;
                note = noteArr[i];
                oscilatorSinus.pitch = note.FrqNote;
                keyNote.KeysSeqMagic(i);
                Debug.Log(noteArr[i].NoteName);
                yield return new WaitForSeconds(speed);
            }
        }



    }
}
