﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyOctav : MonoBehaviour
{
    private int numOctav = 1;//номер октавы
    private int maxOctav = 2;//самая верхняя октава
    private int minOctav = -2;//самая нижняя октава
    [SerializeField] TextMeshProUGUI textNumOctav;
    public void OctavUp()
    {
        numOctav += numOctav < maxOctav ? 1 : 0;
        if (numOctav == 0) numOctav = 1;
        DisplayNumOctav();
    }

    public void OctavDown()
    {
        numOctav -= numOctav > minOctav ? 1 : 0;
        if (numOctav == 0) numOctav = -1;
        DisplayNumOctav();
    }

    public void DisplayNumOctav()
    {        
        textNumOctav.text = numOctav.ToString();
    }

    public int NumOctav
    {
        get { return numOctav; }
    }
}