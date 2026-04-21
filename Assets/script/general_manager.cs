using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class general_manager : MonoBehaviour
{
    static public int pausible_menu_open = 0;
    static public GameObject last_opened_ui;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (pausible_menu_open == 0)
        {
            Time.timeScale = 1.0f;
        }
        else
        {
            Time.timeScale = 0.0f;
        }
    }



}