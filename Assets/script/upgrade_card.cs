using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class upgrade_card : MonoBehaviour
{

    public TextMeshProUGUI card_name;
    public TextMeshProUGUI card_disc;
    public Image card_img;
    public int upgrade_effect_id;

    GameObject player;
    public GameObject parent;

    public GameObject upgrade_selected;

   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("player");
        
        card_name.text = upgrade_selected.GetComponent<upgrades_items>().upgrade_name;
        card_disc.text = upgrade_selected.GetComponent<upgrades_items>().upgrade_desc;
        card_img.sprite = upgrade_selected.GetComponent<upgrades_items>().upgrade_image;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void upgrade()
    {
        string upgrade_effect_id = upgrade_selected.GetComponent<upgrades_items>().upgrade_effect_id;
        int upgrade_effect = upgrade_selected.GetComponent<upgrades_items>().upgrade_effect;

        if (upgrade_effect_id == "hp")
        {
            player.GetComponent<player_movement>().max_health += upgrade_effect;
            player.GetComponent<hp_system>().max_hp += upgrade_effect;
            player.GetComponent<hp_system>().current_hp += upgrade_effect;
        }
        if (upgrade_effect_id == "damage")
        {
            player.GetComponent<player_movement>().base_damage += upgrade_effect;
        }
        if (upgrade_effect_id == "stamina")
        {
            player.GetComponent<player_movement>().max_stamina += upgrade_effect;
            player.GetComponent<player_movement>().stamina += upgrade_effect;
        }
        if (upgrade_effect_id == "stamina_regen")
        {
            player.GetComponent<player_movement>().stamina_regen += upgrade_effect;
        }
        if (upgrade_effect_id == "bounce")
        {
            player.GetComponent<player_movement>().bullet_bounce += upgrade_effect;
        }
        if (upgrade_effect_id == "homing")
        {
            player.GetComponent<player_movement>().bullet_homing += upgrade_effect;
        }
        if (upgrade_effect_id == "projectil_on_melee")
        {
            player.GetComponent<player_movement>().bullet_on_melee += upgrade_effect;
        }
        if(upgrade_effect_id == "dash_cost")
        {
            player.GetComponent<player_movement>().dash_cost -= upgrade_effect;
        }
        if (upgrade_effect_id == "relaod") {
            player.GetComponent<player_movement>().reload_speed -= upgrade_effect;
        }
        if (upgrade_effect_id == "speed")
        {
            player.GetComponent<player_movement>().max_speed += upgrade_effect;
        }

        Destroy(parent);
        general_manager.pausible_menu_open -= 1;
    }

}
