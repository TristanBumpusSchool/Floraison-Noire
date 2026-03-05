using TMPro;
using UnityEngine;

public class upgrade_card : MonoBehaviour
{

    public TextMeshProUGUI card_name;
    public TextMeshProUGUI card_disc;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void upgrade()
    {

        if(upgrade_selected.GetComponent<upgrades_items>().upgrade_effect_id == 1)
        {
            player.GetComponent<player_movement>().max_health += upgrade_selected.GetComponent<upgrades_items>().upgrade_effect;
        }
        if (upgrade_selected.GetComponent<upgrades_items>().upgrade_effect_id == 2)
        {
            player.GetComponent<player_movement>().base_damage += upgrade_selected.GetComponent<upgrades_items>().upgrade_effect;
        }

        Destroy(parent);
        general_manager.pausible_menu_open -= 1;
    }

}
