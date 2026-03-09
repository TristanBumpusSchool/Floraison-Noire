using Unity.VisualScripting;
using UnityEngine;

public class enemy_manager : MonoBehaviour
{

    public GameObject disable_enemies;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject random_enemy()
    {
        int number_of_enemies = disable_enemies.GetComponentsInChildren<Transform>().Length;
        GameObject final_pick = disable_enemies.transform.GetChild(0).gameObject;

        return final_pick;
    }
}
