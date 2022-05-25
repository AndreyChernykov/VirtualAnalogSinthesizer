using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Display : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] textStrArr;// массив строк дисплея
    

    public void Start()
    {
        //Dysplay(0, "test");
        //Dysplay(1, "test");
    }

    public void ToDisplay(int strNum, string text)
    {
        textStrArr[strNum].text = text;
    }

    public void ToDisplay(string text)
    {
        float tmpValue = gameObject.GetComponent<Slider>().value;
        if (gameObject.GetComponent<Slider>().maxValue < 10) tmpValue *= 100;
        textStrArr[textStrArr.Length-1].text = text + "\n" + tmpValue.ToString("00");
    }
}
