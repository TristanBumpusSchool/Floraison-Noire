using System;
using System.Collections.Generic;
using UnityEngine;

public class upgrade_menu_script : MonoBehaviour
{

    public GameObject loot_table_manager;
    public GameObject[] upgrade_cards;
    public List<GameObject> upgrade_to_apply;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        loot_table_manager = GameObject.FindWithTag("loot_table_manager");

        foreach (var item in upgrade_cards)
        {
            while (true)
            {
                GameObject upgrade = loot_table_manager.GetComponent<loot_table>().upgrade_loot_table();

                if (upgrade_to_apply.IndexOf(upgrade) == -1)
                {
                    item.GetComponent<upgrade_card>().upgrade_selected = upgrade;
                    upgrade_to_apply.Add(upgrade);
                    break;
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
