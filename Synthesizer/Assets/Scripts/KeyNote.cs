using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyNote : MonoBehaviour
{
    [SerializeField] Slider sliderDist;//слайдер дисторшена
    [SerializeField] TextMeshProUGUI textDist;
    [SerializeField] Slider sliderDelay;
    [SerializeField] TextMeshProUGUI textDelay;
    [SerializeField] Slider sliderDecay;
    [SerializeField] TextMeshProUGUI textDecay;
    [SerializeField] GameObject seqObj;
    [SerializeField] Button btnSeq;
    [SerializeField] Button btnRecSeq;
    private int numKeySeqToClick;//номер нажатой кнопки на секвенсоре
    private bool toRec = false;//нажата ли кнопка записи секвенции
    private Filters filters;//скрипт фильтров
    private float positionBtnSeqX = 40;//расстояние между кнопками секвенсора
    private int bpi = 430;//скорость секвенсора
    private bool bpiCount = false;
    PlayNote playNote;
    Sequencer sequencer;
    private Button[] btnSeqArr;

    public void Start()
    {
        playNote = gameObject.GetComponent<PlayNote>();
        sequencer = gameObject.GetComponent<Sequencer>();
        filters = GameObject.Find("OsciliatorSinus").GetComponent<Filters>();
        btnSeqArr = new Button[sequencer.SeqLength];

        CreateSequencer();

    }

   

    public void ClickKeyNote(string nameKey)//обрабатываем нажатие клавиши с нотой и получаем ее название
    {        
        playNote.PlayToNote(nameKey);

        if (toRec && numKeySeqToClick < sequencer.SeqLength)//при включённой записи шагаем по секвенции
        {
            KeysSeqMagic(numKeySeqToClick);
            numKeySeqToClick++;
        }

        SliderDist();
        SliderDelay();
        SliderDecay();
    }

    public void NoClickNote()//при отпускании клавиши с нотой
    {
        playNote.StopPlayNote();
    }

    public void SliderDist()//дисторшен
    {
        filters.DistValue = sliderDist.value;
        textDist.text = "Dist " + (sliderDist.value * 100).ToString("0");
        filters.Distortion();//дисторшен фильтр
    }


    public void SliderDelay()
    {
        textDelay.text = "Delay " + sliderDelay.value.ToString("0");
        filters.Delay = sliderDelay.value;
    }

    public void SliderDecay()
    {
        filters.Decay = sliderDecay.value;
        textDecay.text = "Decay " + (sliderDecay.value * 100).ToString("0");
    }

    public void PlaySeq()
    {
        toRec = true;
        Rec();
        sequencer.Play();

    }

    public void StopSeq()
    {
        sequencer.Stop();
    }

    private void CreateSequencer()//расставляем кнопки для секвенсора
    {
        float x = 0;
        for(int i = 0; i < sequencer.SeqLength; i++)
        {
            Button btnNew = Instantiate(btnSeq);
            btnNew.transform.SetParent(seqObj.transform);
            btnNew.transform.localPosition = new Vector3(x, 0, 0);
            btnNew.name = i.ToString();
            btnSeqArr[i] = btnNew;
            x += positionBtnSeqX;
        }
    }

    public void KeysSeqMagic(int numKey)//меняем цвет проигрываемой кнопке
    {       
        for (int i = 0; i < btnSeqArr.Length; i++) btnSeqArr[i].GetComponent<Image>().color = Color.white;
        if(toRec)btnSeqArr[numKey].GetComponent<Image>().color = Color.red;//если включина запись
        else btnSeqArr[numKey].GetComponent<Image>().color = Color.green;//если включино воспроизведение
    }

    public int NumKeySeqToClick
    {
        set { numKeySeqToClick = value;}
        get { return numKeySeqToClick; }
    }

    public void Rec()//при нажатии на кнопку записи секвенции
    {
        if (toRec)
        {
            
            btnRecSeq.GetComponent<Image>().color = Color.white;
            toRec = false;
        }
        else
        {
            numKeySeqToClick = 0;
            btnRecSeq.GetComponent<Image>().color = Color.red;
            toRec = true;
        }

        StopSeq();
    }

    public void Bpi(string s)//устанавливаем скорость секвенсора
    {
        bpiCount = true;
        //bpi += s.Equals("+") ? 1 : -1;
        //sequencer.BPI = bpi;
        //Debug.Log("bpi = " + bpi);
        StartCoroutine(BpiCounter(s));
    }

    public void StopBpiCount()
    {
        bpiCount = false;
        StopCoroutine(BpiCounter(""));
    }

    public IEnumerator BpiCounter(string s)
    {
        while(bpi < 440 && bpi > 1 && bpiCount)
        {
            bpi += s.Equals("+") ? 1 : -1;
            sequencer.BPI = bpi;
            Debug.Log("bpi = " + bpi);
            yield return new WaitForSeconds(0.2f);
        }

        if (bpi <= 1 && s.Equals("+")) bpi++;
        if(bpi >= 440 && s.Equals("-")) bpi--;
        Debug.Log("bpi = " + bpi);
        StopCoroutine(BpiCounter(s));


    }


}
