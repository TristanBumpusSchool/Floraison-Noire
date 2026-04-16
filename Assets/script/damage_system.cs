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

    private void OnTriggerExit(Collider other)
    {
        print("Collision");
        if (other.GetComponent<hp_system>() != null & other.tag != source){
            if (other.tag == "player") {
                print("S");
                if (!other.GetComponent<player_movement>().blocking || other.GetComponent<player_movement>().blocking & other.GetComponent<player_movement>().stamina < other.GetComponent<player_movement>().block_cost)
                {

                    other.GetComponent<player_movement>().blocking = false;

                    other.GetComponent<hp_system>().current_hp -= damage;
                    if(other.GetComponent<hp_system>().sound_to_play_on_hit != null)
                    {
                        other.GetComponent<AudioSource>().PlayOneShot(other.GetComponent<hp_system>().sound_to_play_on_hit);
                    }
                }
                else
                {
                    other.GetComponent<player_movement>().stamina -= other.GetComponent<player_movement>().block_cost;
                    other.GetComponent<player_movement>().parry();
                }
            }
            else
            {
                print(damage);
                other.GetComponent<hp_system>().current_hp -= damage;
                if (other.GetComponent<hp_system>().sound_to_play_on_hit != null)
                {
                    other.GetComponent<AudioSource>().PlayOneShot(other.GetComponent<hp_system>().sound_to_play_on_hit);
                }
            }
            }
    }
}
