using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class settings : MonoBehaviour
{

    static public bool open_settings = false;
    Vector3 start_pos = Vector3.zero;
    public GameObject first_button;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        start_pos = transform.position;
        transform.position = new Vector3(0, 10000, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void click_fullscreen() {
        Screen.fullScreen = !Screen.fullScreen;
        print("F");
    }
    public void click_resolution() {
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
        print("R");
    }

    public void on_settings_input(InputAction.CallbackContext contex)
    {
        if (contex.performed)
        {
            if (!open_settings)
            { 
                open_settings = true;
                GameObject.FindGameObjectWithTag("event_system").GetComponent<EventSystem>().SetSelectedGameObject(first_button);
                transform.position = start_pos;
                general_manager.pausible_menu_open += 1;
               
            }
            else if (open_settings)
            {
                open_settings = false;
                transform.position = new Vector3(0, 10000, 0);
                general_manager.pausible_menu_open -= 1;
                if(general_manager.last_opened_ui != null)
                {
                    GameObject.FindGameObjectWithTag("event_system").GetComponent<EventSystem>().SetSelectedGameObject(general_manager.last_opened_ui.GetComponent<UI>().first_button);
                }
            }
        }
    }
}
