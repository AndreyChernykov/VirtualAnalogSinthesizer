using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSeq : MonoBehaviour
{
    private KeyNote keyNote;

    public void Start()
    {
        keyNote = GameObject.Find("Canvas").GetComponent<KeyNote>();
    }
    public void ClickBtnSeq()// обработка нажатий кнопок секвенсора
    {
        keyNote.NumKeySeqToClick = System.Int32.Parse(this.name.ToString());
        this.GetComponent<Image>().color = Color.red;
    }
}
