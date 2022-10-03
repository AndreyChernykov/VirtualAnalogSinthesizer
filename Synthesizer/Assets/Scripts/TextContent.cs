using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextContent
{
    public const string version = "v0.91";
    public const string about = "About the application:\nVirtual analog 5 octave synthesizer " +
         "with two oscillators (sine waves and saws)," +
         "16 step sequencer and square wave LFO";

    public string About
    {
        get { return about; }
    }

    public string Version
    {
        get { return version; }
    }
}
