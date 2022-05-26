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
    float tmpValue;

    public void Start()
    {

    }

    public void ToDisplay(int strNum, string text)
    {
        textStrArr[strNum].text = text;
    }

    public void ToDisplay(string text)//отображение значения на дисплее
    {
        tmpValue = gameObject.GetComponent<Slider>().value;
        float maxValue = gameObject.GetComponent<Slider>().maxValue;

        if (maxValue < 10) tmpValue *= 100;
        else if(maxValue > 1000) tmpValue /= 100;
        textStrArr[textStrArr.Length-1].text = text + "\n" + tmpValue.ToString("00");

        FaiderRotation();
    }

    private void FaiderRotation()//вращение фэйдера
    {
        float n;
        if (faidRoto > tmpValue) n = tmpValue;
        else n = -tmpValue;
        faidRoto = tmpValue;
        faider.transform.rotation *= new Quaternion(0, 0, n * Time.deltaTime, 1);
    }
}
