using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyNote : MonoBehaviour
{
    [SerializeField] Slider sliderDist;//слайдер дисторшена
    //[SerializeField] TextMeshProUGUI textDist;
    [SerializeField] Slider sliderDelay;
    //[SerializeField] TextMeshProUGUI textDelay;
    [SerializeField] Slider sliderDecay;
    //[SerializeField] TextMeshProUGUI textDecay;
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
    [SerializeField] Button btnSquareLFO;
    [SerializeField] Slider sliderFrequency;
    [SerializeField] Slider sliderAmplitude;
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
    LFO lfo, lfo2;
    LFO lfoChoice;
    Display display;
    private Button[] btnSeqArr;

    public void Start()
    {
        playNote = gameObject.GetComponent<PlayNote>();
        sequencer = gameObject.GetComponent<Sequencer>();
        filters = GameObject.Find("OsciliatorSinus").GetComponent<Filters>();
        filters2 = GameObject.Find("OsciliatorSaw").GetComponent<Filters>();
        lfo = GameObject.Find("OsciliatorSinus").GetComponent<LFO>();
        lfo2 = GameObject.Find("OsciliatorSaw").GetComponent<LFO>();
        btnSeqArr = new Button[sequencer.SeqLength];
        display = gameObject.GetComponent<Display>();
        display.ToDisplay(1, "bpm " + bpm);

        textBPM.text = bpm.ToString();
        CreateSequencer();

    }

    public void SinOscOnOff()//вкл-выкл осциллятор синусойды
    {
        Color c;
        if (onSinOsc)
        {
            c = Color.white;
            onSinOsc = false;
        }
        else
        {
            c = Color.red;
            onSinOsc = true;
        }
        btnSinOsc.GetComponent<Image>().color = c;
    }

    public bool OnSinOsc
    {
        get { return onSinOsc; }
    }

    public void SawOscOnOff()//вкл-выкл осциллятор пилы
    {
        Color c;
        if (onSawOsc)
        {
            c = Color.white;
            onSawOsc = false;
        }
        else
        {
            c = Color.red;
            onSawOsc = true;
        }
        btnSawOsc.GetComponent<Image>().color = c;
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
        Filters fil = oscForFiltrs.Equals("Sinus") ? filters2 : filters;
        lfoChoice = oscForFiltrs.Equals("Sinus") ? lfo2 : lfo;

        SliderDist(fil);
        SliderDelay(fil);
        SliderDecay(fil);
        SliderCutoff(fil);
        SliderResonance(fil);

        FrequencyLFO(lfoChoice);
        AmplitudeLFO(lfoChoice);


    }

    public void NoClickNote()//при отпускании клавиши с нотой
    {
        playNote.StopPlayNote();
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
        Color c;
        for (int i = 0; i < btnSeqArr.Length; i++) btnSeqArr[i].GetComponent<Image>().color = Color.white;
        if(toRec)c = Color.red;//если включина запись
        else c = Color.green;//если включино воспроизведение
        btnSeqArr[numKey].GetComponent<Image>().color = c;
    }

    public int NumKeySeqToClick
    {
        set { numKeySeqToClick = value;}
        get { return numKeySeqToClick; }
    }

    public void Rec()//при нажатии на кнопку записи секвенции
    {
        Color c;
        if (toRec)
        {           
            c = Color.white;
            toRec = false;
        }
        else
        {
            numKeySeqToClick = 0;
            c = Color.red;
            toRec = true;
        }
        btnRecSeq.GetComponent<Image>().color = c;
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
            display.ToDisplay(1, "bpm " + bpm);
            yield return new WaitForSeconds(0.15f);
        }

        if (bpm <= 1 && s.Equals("+")) bpm++;
        if(bpm >= maxBpm && s.Equals("-")) bpm--;
        textBPM.text = bpm.ToString();
        StopCoroutine(BpiCounter(s));

    }

    public void ChoiceFilterOsc()//выбор переключения фильтра на осциллятор
    {
        string s;
        Filters fil;

        oscForFiltrs = btnFilter.GetComponentInChildren<Text>().GetComponent<Text>().text;
        if (oscForFiltrs.Equals("Sinus"))
        {
            s = "Saw";
            fil = filters2;
            
            lfoChoice = lfo2;
        }
        else
        {
            s = "Sinus";
            fil = filters;

            lfoChoice = lfo;
        }
        btnFilter.GetComponentInChildren<Text>().GetComponent<Text>().text = s;

        //Color color = lfoChoice.OnLFO ? Color.red : Color.white;
        btnSquareLFO.GetComponent<Image>().color = lfoChoice.OnLFO ? Color.red : Color.white;

        sliderCutoff.value = fil.Cutoff;
        sliderReso.value = fil.Resonance;
        sliderDist.value = fil.DistValue;
        sliderDelay.value = fil.Delay;
        sliderDecay.value = fil.Decay;

        sliderAmplitude.value = lfoChoice.Amplitude;
        sliderFrequency.value = lfoChoice.TimeOscillation;

        Debug.Log(oscForFiltrs);
    }

    private void SliderCutoff(Filters f)
    {
        f.Cutoff = sliderCutoff.value;
    }

    private void SliderResonance(Filters f)
    {
        f.Resonance = sliderReso.value;
    }


    private void SliderDist(Filters f)//дисторшен
    {
        f.DistValue = sliderDist.value;
        f.Distortion();//дисторшен фильтр
        //textDist.text = "Dist " + (sliderDist.value * 100).ToString("0");
    }

    private void SliderDelay(Filters f)
    {
        //textDelay.text = "Delay " + sliderDelay.value.ToString("0");
        f.Delay = sliderDelay.value;
    }

    private void SliderDecay(Filters f)
    {
        f.Decay = sliderDecay.value;
        //textDecay.text = "Decay " + (sliderDecay.value * 100).ToString("0");
    }

    public void BtnLFO()//кнопка включения лфо
    {
        
        Color color;

        if (!lfoChoice.OnLFO)
        {
            lfoChoice.OnLFO = true;
            lfoChoice.StartLFO();
            color = Color.red;
        }
        else
        {
            lfoChoice.OnLFO = false;
            color = Color.white;
        }
        btnSquareLFO.GetComponent<Image>().color = color;


    }

    private void FrequencyLFO(LFO l)//частота ЛФО
    {
        l.TimeOscillation = sliderFrequency.value;
    }

    private void AmplitudeLFO(LFO l)//амплитуда ЛФО
    {
        l.Amplitude = sliderAmplitude.value;
    }

    public void SliderVisable(GameObject slid)
    {
        slid.gameObject.SetActive(true);
        //if (slid.activeSelf) slid.SetActive(false);
        //else slid.gameObject.SetActive(true);
    }

    public void SliderInVisable(GameObject slid)
    {
        slid.gameObject.SetActive(false);
    }



}
