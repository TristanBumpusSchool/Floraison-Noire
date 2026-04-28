using System.Collections.Generic;
using UnityEngine;

public class loot_table : MonoBehaviour
{
    [Header("Upgrades")]
    public GameObject[] upgrades_commun;
    public GameObject[] upgrades_rare;
    public GameObject[] upgrades_legendary;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public GameObject upgrade_loot_table(int commun_chance = 100,int rare_chance = 0,int legendary_chance = 0) {
        int chance = Random.Range(1, 100);
        print(chance);
        if (chance <= commun_chance)
        {
            int upgrade_id = Random.Range(0, upgrades_commun.Length - 1);
            GameObject final_upgrade = upgrades_commun[upgrade_id];
            return final_upgrade;
        }
        if (chance <= rare_chance + commun_chance)
        {
            int upgrade_id = Random.Range(0, upgrades_rare.Length - 1);
            GameObject final_upgrade = upgrades_rare[upgrade_id];
            return final_upgrade;
        }
        if (chance <= legendary_chance + rare_chance + commun_chance)
        {
            int upgrade_id = Random.Range(0, upgrades_legendary.Length - 1);
            GameObject final_upgrade = upgrades_legendary[upgrade_id];
            return final_upgrade;
        }

        return null;
    }
}
