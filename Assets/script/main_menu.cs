using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class main_menu : MonoBehaviour
{

    public Animator animator;
    public EventSystem event_system;
    public GameObject[] default_buttons;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void to_game()
    {
        SceneManager.LoadScene(1);
    }

    public void tut()
    {
        animator.Play("main_menu_tut");
        event_system.firstSelectedGameObject = default_buttons[2];
        event_system.SetSelectedGameObject(default_buttons[2]);
    }

    public void return_tut()
    { 
        animator.Play("tut_main_menu");
        event_system.firstSelectedGameObject = default_buttons[0];
        event_system.SetSelectedGameObject(default_buttons[0]);
    }
}
