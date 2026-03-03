using TMPro;
using UnityEngine;

public class upgrade_card : MonoBehaviour
{

    public TextMeshProUGUI card_name;
    public TextMeshProUGUI card_disc;

    GameObject loot_table_manager;
    GameObject player;
    public GameObject parent;

    public GameObject upgrade_selected;

   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("player");
        loot_table_manager = GameObject.FindWithTag("loot_table_manager");
        Debug.Log(loot_table_manager.GetComponent<loot_table>().upgrade_loot_table());
        upgrade_selected = Instantiate(loot_table_manager.GetComponent<loot_table>().upgrade_loot_table(),transform);
        
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

        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Destroy(parent);
    }

}
