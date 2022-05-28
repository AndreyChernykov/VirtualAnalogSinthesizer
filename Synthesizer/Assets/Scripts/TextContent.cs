using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextContent
{
    public const string version = "v0.9";
    public const string about = "О приложении:\nВиртуальный аналоговый синтезатор " +
        "с двумя осцилляторами (синусойды и пилы), " +
        "16-ти шаговым секвенсором и LFO квадратной волны";

    public string About
    {
        get { return about; }
    }

    public string Version
    {
        get { return version; }
    }
}
