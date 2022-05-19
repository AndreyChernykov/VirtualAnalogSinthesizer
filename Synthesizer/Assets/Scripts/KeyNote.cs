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
    [SerializeField] Slider sliderCutoff;
    [SerializeField] Slider sliderReso;
    [SerializeField] GameObject seqObj;
    [SerializeField] Button btnSeq;
    [SerializeField] Button btnRecSeq;
    [SerializeField] Button btnPlaySeq;
    [SerializeField] TextMeshProUGUI textBPM;
    [SerializeField] Button btnSinOsc;
    [SerializeField] Button btnSawOsc;
    [SerializeField] Button btnFilter;
    private string oscForFiltrs = "";//какой осциллятор выбран для фильтра катоф
    private int numKeySeqToClick;//номер нажатой кнопки на секвенсоре
    private bool toRec = false;//нажата ли кнопка записи секвенции
    private Filters filters, filters2;//фильтра
    private float positionBtnSeqX = 40;//расстояние между кнопками секвенсора
    private int bpm = 120;//скорость секвенсора
    private int maxBpm = 440;//максимальная скорость секвенсора
    private bool bpiCount = false;
    private bool onSinOsc = false;//включён ли осциллятор синусойды
    private bool onSawOsc = false;//включён ли осциллятор пилы
    PlayNote playNote;
    Sequencer sequencer;
    private Button[] btnSeqArr;

    public void Start()
    {
        playNote = gameObject.GetComponent<PlayNote>();
        sequencer = gameObject.GetComponent<Sequencer>();
        filters = GameObject.Find("OsciliatorSinus").GetComponent<Filters>();
        filters2 = GameObject.Find("OsciliatorSaw").GetComponent<Filters>();
        btnSeqArr = new Button[sequencer.SeqLength];

        textBPM.text = bpm.ToString();
        CreateSequencer();

    }

    public void SinOscOnOff()//вкл-выкл осциллятор синусойды
    {
        if (onSinOsc)
        {
            btnSinOsc.GetComponent<Image>().color = Color.white;
            onSinOsc = false;
        }
        else
        {
            btnSinOsc.GetComponent<Image>().color = Color.red;
            onSinOsc = true;
        }
    }

    public bool OnSinOsc
    {
        get { return onSinOsc; }
    }

    public void SawOscOnOff()//вкл-выкл осциллятор пилы
    {
        if (onSawOsc)
        {
            btnSawOsc.GetComponent<Image>().color = Color.white;
            onSawOsc = false;
        }
        else
        {
            btnSawOsc.GetComponent<Image>().color = Color.red;
            onSawOsc = true;
        }
    }

    public bool OnSawOsc
    {
        get { return onSawOsc; }
    }


    public void ClickKeyNote(string nameKey)//обрабатываем нажатие клавиши с нотой и получаем ее название
    {        
        playNote.PlayToNote(nameKey);

        if (toRec && numKeySeqToClick < sequencer.SeqLength)//при включённой записи шагаем по секвенции
        {
            KeysSeqMagic(numKeySeqToClick);
            numKeySeqToClick++;
        }


    }

    public void FixedUpdate()
    {
        SliderDist(filters, filters2);
        SliderDelay(filters, filters2);
        SliderDecay(filters, filters2);
        Cutoff();
        Resonance();
    }

    public void NoClickNote()//при отпускании клавиши с нотой
    {
        playNote.StopPlayNote();
    }

    public void SliderDist(params Filters[] fil)//дисторшен
    {
        foreach(Filters f in fil)
        {
            f.DistValue = sliderDist.value;
            f.Distortion();//дисторшен фильтр
        }
        textDist.text = "Dist " + (sliderDist.value * 100).ToString("0");
    }


    public void SliderDelay(params Filters[] fil)
    {
        textDelay.text = "Delay " + sliderDelay.value.ToString("0");
        foreach(Filters f in fil) f.Delay = sliderDelay.value;
    }

    public void SliderDecay(params Filters[] fil)
    {
        foreach(Filters f in fil) f.Decay = sliderDecay.value;
        textDecay.text = "Decay " + (sliderDecay.value * 100).ToString("0");
    }

    public void PlaySeq()
    {
        toRec = true;
        Rec();
        sequencer.Play();
        btnPlaySeq.interactable = false;
    }

    public void StopSeq()
    {
        sequencer.Stop();
        btnPlaySeq.interactable = true;
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

    public void Bpm(string s)//устанавливаем скорость секвенсора
    {
        bpiCount = true;
        StartCoroutine(BpiCounter(s));
    }

    public void StopBpiCount()
    {
        bpiCount = false;
        StopCoroutine(BpiCounter(""));
    }

    public IEnumerator BpiCounter(string s)
    {
        while(bpm < maxBpm && bpm > 1 && bpiCount)
        {
            bpm += s.Equals("+") ? 1 : -1;
            sequencer.BPM = bpm;
            textBPM.text = bpm.ToString();
            yield return new WaitForSeconds(0.15f);
        }

        if (bpm <= 1 && s.Equals("+")) bpm++;
        if(bpm >= maxBpm && s.Equals("-")) bpm--;
        textBPM.text = bpm.ToString();
        StopCoroutine(BpiCounter(s));

    }

    public void ChoiceFilterOsc()//выбор переключения фильтра на осциллятор
    {
        oscForFiltrs = btnFilter.GetComponentInChildren<Text>().GetComponent<Text>().text;
        if (oscForFiltrs.Equals("Sinus"))
        {
            btnFilter.GetComponentInChildren<Text>().GetComponent<Text>().text = "Saw";
            sliderCutoff.value = filters2.Cutoff;
            sliderReso.value = filters2.Resonance;
        }
        else
        {
            btnFilter.GetComponentInChildren<Text>().GetComponent<Text>().text = "Sinus";
            sliderCutoff.value = filters.Cutoff;
            sliderReso.value = filters.Resonance;
        }
        Debug.Log(oscForFiltrs);
    }

    private void Cutoff()
    {
        if (oscForFiltrs.Equals("Sinus")) filters2.Cutoff = sliderCutoff.value;
        else filters.Cutoff = sliderCutoff.value;
    }

    private void Resonance()
    {
        if (oscForFiltrs.Equals("Sinus")) filters2.Resonance = sliderReso.value;
        else filters.Resonance = sliderReso.value;
    }
}
