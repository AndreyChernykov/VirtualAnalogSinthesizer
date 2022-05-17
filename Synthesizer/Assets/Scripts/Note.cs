using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note
{
    private string noteName;// название ноты
    private int octav;//номер октавы
    private float frqNote;//частота ноты

    public Note(string noteName, int octav)
    {
        this.noteName = noteName;
        this.octav = octav;
        //this.frqNote = frqNote;

        this.frqNote = new FrequencyNotes().GetFrequency(noteName);

        Frequency();
    }

    public void Frequency()//получаем частоту нажатой ноты
    {
        switch (octav)
        {
            case -3:
                frqNote /= 8;
                break;
            case -2:
                frqNote /= 4;
                break;
            case -1:
                frqNote /= 2;
                break;
            case 1:
                frqNote *= 1;
                break;
            case 2:
                frqNote *= 2;
                break;
            case 3:
                frqNote *= 4;
                break;
            case 4:
                frqNote *= 8;
                break;
            default:
                frqNote = 0;
                break;
        }

    }

    public float FrqNote
    {
        get { return frqNote; }
    }

    public string NoteName
    {       
        get { return noteName + octav + "  " + frqNote; }
    }
}

public class FrequencyNotes
{
    private Dictionary<string, float> frqNotes;

    public FrequencyNotes()
    {
        frqNotes = new Dictionary<string, float>()//частоты нот первой октавы
        {
            {"Q", 0 },
            {"A", 1f },
            {"A#", 1.05f },
            {"B", 1.12f },
            {"C", 0.594f },
            {"C#", 0.63f },
            {"D", 0.666f },
            {"D#", 0.71f },
            {"E", 0.75f },
            {"F", 0.79f },
            {"F#", 0.84f },
            {"G", 0.89f },
            {"G#", 0.94f }

        };       
    }

    public float GetFrequency(string name)
    {
        return frqNotes[name];
    }

}
