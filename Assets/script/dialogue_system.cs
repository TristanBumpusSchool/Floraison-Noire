using TMPro;
using UnityEngine;

public class dialogue_system : MonoBehaviour
{

    public TextMeshProUGUI text_ui;
    public string text_to_display;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(text_to_display != "" & !IsInvoking("next_letter")) {
            Invoke("next_letter", .1f);
        }
        if (text_to_display != "" & !IsInvoking("end_text") & text_ui.text != "")
        {
            Invoke("end_text", 5f);
        }
    }

    void next_letter()
    {
        text_ui.text += text_to_display[0];
        text_to_display = text_to_display.Substring(1);
    }

    void end_text()
    {
        text_ui.text = "";
    }

}
