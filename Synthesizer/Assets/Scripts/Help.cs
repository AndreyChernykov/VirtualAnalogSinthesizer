using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Help : MonoBehaviour
{
    TextContent textContent;
    [SerializeField] Text textAbout;

    public void Start()
    {
        textContent = new TextContent();
        textAbout.text = textContent.About + '\n' + textContent.Version;
    }

    public void Back()//обработка нажатия на кнопку Back
    {
        SceneManager.LoadScene("Synthesizer");
    }
}
