using UnityEngine;

public class general_manager : MonoBehaviour
{

    static public int pausible_menu_open = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pausible_menu_open == 0)
        {
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;   
        }
        else
        {
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
