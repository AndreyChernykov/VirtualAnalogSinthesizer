using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Display : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] textStrArr;// массив строк дисплея
    [SerializeField] Image faider;
    private float faidRoto = 0;

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

        float n;
        if (faidRoto > tmpValue) n = tmpValue;
        else n = -tmpValue;
        faidRoto = tmpValue;
        faider.transform.rotation *= new Quaternion(0, 0, n * Time.deltaTime, 1);
    }

    private void FaiderRotation()
    {
        faider.transform.rotation *= new Quaternion(0, 0, 1* Time.deltaTime, 1);
    }
}
