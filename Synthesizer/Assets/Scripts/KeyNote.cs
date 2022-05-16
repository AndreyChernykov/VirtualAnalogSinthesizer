using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyNote : MonoBehaviour
{
    PlayNote playNote;

    public void Start()
    {
        playNote = gameObject.GetComponent<PlayNote>();
    }

    public void ClickKeyNote(string nameKey)//обрабатываем нажатие клавиши с нотой и получаем ее название
    {        
        playNote.PlayToNote(nameKey);
    }

    public void NoClickNote()//при отпускании клавиши с нотой
    {
        playNote.StopPlayNote();
    }


}
