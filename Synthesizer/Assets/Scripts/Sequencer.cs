using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer : MonoBehaviour
{
    private AudioSource oscilatorSinus;//осцилятор синусойды
    private float speed = 1;//скорость переключения нот
    private int seqLength = 16;//количество шагов секвенсора
    private bool toPlaySeq = false;
    Note note;
    private Note[] noteArr;

    private void TestSeq()
    {
        
        noteArr[0] = new Note("C", 1);
        noteArr[1] = new Note("C", 2);
        noteArr[3] = new Note("C", 3);
    }

    public void Start()
    {
        noteArr = new Note[seqLength];
        for (int i = 0; i < noteArr.Length; i++) noteArr[i] = new Note("C", 0);

        oscilatorSinus = GameObject.Find("OsciliatorSinus").GetComponent<AudioSource>();

        TestSeq();
    }

    public void PlaySeq()
    {
        if (!toPlaySeq)
        {
            toPlaySeq = true;
            StartCoroutine(PlaySequence());
        }

    }

    public void StopSeq()
    {
        toPlaySeq = false;
        StopCoroutine(PlaySequence());
        oscilatorSinus.volume = 0;

    }

    private IEnumerator PlaySequence()
    {
        oscilatorSinus.volume = 1;
        while (toPlaySeq)
        {
            for (int i = 0; i < noteArr.Length; i++)
            {
                note = noteArr[i];
                oscilatorSinus.pitch = note.FrqNote;
                Debug.Log(noteArr[i].NoteName);
                yield return new WaitForSeconds(speed);
            }
        }



    }
}
