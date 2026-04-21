using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class settings : MonoBehaviour
{

    static public bool open_settings = false;
    static public float mouse_sensitivy = 1f;
    public Slider sensitivy;
    Vector3 start_pos = Vector3.zero;
    public GameObject first_button;
    public GameObject[] sections;
    int current_section = 0;

    GameObject event_system;



    void change_section()
    {
        foreach (var s in sections)
        {
            s.SetActive(false);
        }
        sections[current_section].SetActive(true);
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        event_system = GameObject.FindGameObjectWithTag("event_system");
        start_pos = transform.position;
        transform.position = new Vector3(0, 10000, 0);
    }

    // Update is called once per frame
    void Update()
    {
        mouse_sensitivy = sensitivy.value;
        if(event_system != null ) 
        { 
            if (event_system.GetComponent<EventSystem>().currentSelectedGameObject.name == "graphics" & current_section != 0) {
            current_section = 0;
            change_section();
            }
            if (event_system.GetComponent<EventSystem>().currentSelectedGameObject.name == "sounds" & current_section != 1) {
                current_section = 1;
                change_section();
            }
            if (event_system.GetComponent<EventSystem>().currentSelectedGameObject.name == "controls" & current_section != 2) {
                current_section = 2;
                change_section();
            }
            if (event_system.GetComponent<EventSystem>().currentSelectedGameObject.name == "exit" & current_section != 3)
            {
                current_section = 3;
                change_section();
            }
        }

    }

    public void click_fullscreen() {
        Screen.fullScreen = !Screen.fullScreen;
        print("F");
    }
    public void click_resolution() {
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
        print("R");
    }

    public void click_resume()
    {
        open_settings = false;
        transform.position = new Vector3(0, 10000, 0);
        first_button = event_system.GetComponent<EventSystem>().currentSelectedGameObject;
        general_manager.pausible_menu_open -= 1;
        if (general_manager.last_opened_ui != null)
        {
            event_system.GetComponent<EventSystem>().SetSelectedGameObject(general_manager.last_opened_ui.GetComponent<UI>().first_button);
        }
    }

    public void on_settings_input(InputAction.CallbackContext contex)
    {
        if (contex.performed)
        {
            if (!open_settings)
            { 
                open_settings = true;
                event_system.GetComponent<EventSystem>().SetSelectedGameObject(first_button);
                transform.position = start_pos;
                general_manager.pausible_menu_open += 1;
               
            }
            else if (open_settings)
            {
                open_settings = false;
                transform.position = new Vector3(0, 10000, 0);
                first_button = event_system.GetComponent<EventSystem>().currentSelectedGameObject;
                general_manager.pausible_menu_open -= 1;
                if(general_manager.last_opened_ui != null)
                {
                    event_system.GetComponent<EventSystem>().SetSelectedGameObject(general_manager.last_opened_ui.GetComponent<UI>().first_button);
                }
            }
        }
    }
}
