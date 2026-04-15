using UnityEngine;

public class interactble : MonoBehaviour
{

    public string type;
    public GameObject ui;

    GameObject player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {

        if (player.GetComponent<player_movement>().interaction_object == gameObject) { 
            ui.SetActive(true);
        }
        else
        {
            ui.SetActive(false);
        }
    }



    public void activate(GameObject player)
    {
        if(type == "weapon")
        {
            if(tag == "melee slot")
            {
                Vector3 start_pos = transform.position;
                
                player.GetComponent<player_movement>().melee_slot.transform.GetChild(0).transform.position = start_pos;
                player.GetComponent<player_movement>().melee_slot.transform.GetChild(0).GetComponentInChildren<Collider>().enabled = true;
                player.GetComponent<player_movement>().melee_slot.transform.GetChild(0).transform.parent = null;
                
                transform.parent.transform.parent = player.GetComponent<player_movement>().melee_slot.transform;
                transform.GetComponent<Collider>().enabled = false;
                transform.parent.transform.localPosition = Vector3.zero;
            }
            if (tag == "range slot")
            {
                Vector3 start_pos = transform.position;

                player.GetComponent<player_movement>().range_slot.transform.GetChild(0).transform.position = start_pos;
                player.GetComponent<player_movement>().range_slot.transform.GetChild(0).GetComponentInChildren<Collider>().enabled = true;
                player.GetComponent<player_movement>().range_slot.transform.GetChild(0).transform.parent = null;

                transform.parent.transform.parent = player.GetComponent<player_movement>().range_slot.transform;
                transform.GetComponent<Collider>().enabled = false;
                transform.parent.transform.localPosition = Vector3.zero;
            }
        }
    }
}
