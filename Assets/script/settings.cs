using UnityEngine;

public class settings : MonoBehaviour
{

    static public bool open_settings = false;
    Vector3 start_pos = Vector3.zero;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        start_pos = transform.position;
        transform.position = new Vector3(0, 10000, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Backspace) & !open_settings)
        {
            open_settings = true;
            transform.position = start_pos;
            general_manager.pausible_menu_open += 1;
        }
        else if (Input.GetKeyUp(KeyCode.Backspace) & open_settings) { 
            open_settings = false;
            transform.position = new Vector3(0, 10000, 0);
            general_manager.pausible_menu_open -= 1;
        }
    }
}
