using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class stats_screen : MonoBehaviour
{

    public player_movement player_script;
    public TextMeshProUGUI damage_text;
    public TextMeshProUGUI health_text;

    bool is_open = false;
    Vector3 start_pos;

    //public TextMeshProUGUI ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        start_pos = transform.position;
        transform.position = new Vector3(0, 10000, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Update stats screen ui

        transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = (player_script.base_damage + player_script.weapon_damage).ToString();
        transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = player_script.max_health.ToString();
        transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = player_script.max_stamina.ToString();
        transform.GetChild(9).GetComponent<TextMeshProUGUI>().text = player_script.reload_speed.ToString() + "s";
    }

    public void on_inv_input(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!settings.open_settings & !is_open)
            {
                general_manager.pausible_menu_open += 1;
                transform.position = start_pos;
                is_open = true;

            }
            else if (is_open & !settings.open_settings)
            {
                general_manager.pausible_menu_open -= 1;
                transform.position = new Vector3(0, 10000, 0);
                is_open = false;
            }
        }
    }

}
