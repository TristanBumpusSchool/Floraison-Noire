using UnityEngine;

public class damage_system : MonoBehaviour
{


    public float damage = 1;
    public string source = "none";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<hp_system>() != null & other.tag != source){
            if (other.tag == "player") {
                if (!other.GetComponent<player_movement>().blocking & other.GetComponent<player_movement>().stamina > other.GetComponent<player_movement>().block_cost)
                {
                    other.GetComponent<hp_system>().current_hp -= damage;
                }
                else
                {
                    other.GetComponent<player_movement>().stamina -= other.GetComponent<player_movement>().block_cost;
                }
            }
            else
            {
                other.GetComponent<hp_system>().current_hp -= damage;
            }
            }
    }
}
