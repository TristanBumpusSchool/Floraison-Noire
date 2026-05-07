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
                player.GetComponent<player_movement>().melee_slot.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
                player.GetComponent<player_movement>().melee_slot.transform.GetChild(0).GetChild(0).GetComponent<Collider>().enabled = true;
                player.GetComponent<player_movement>().melee_slot.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().enabled = true;
                player.GetComponent<player_movement>().melee_slot.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                player.GetComponent<player_movement>().melee_slot.transform.GetChild(0).transform.parent = null;
                
                transform.parent.transform.parent = player.GetComponent<player_movement>().melee_slot.transform;
                transform.GetComponent<Collider>().enabled = false;
                transform.GetComponent<MeshRenderer>().enabled = false;
                transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
                transform.parent.transform.localPosition = Vector3.zero;
            }
            if (tag == "range slot")
            {
                Vector3 start_pos = transform.position;

                player.GetComponent<player_movement>().range_slot.transform.GetChild(0).transform.position = start_pos;
                player.GetComponent<player_movement>().range_slot.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
                player.GetComponent<player_movement>().range_slot.transform.GetChild(0).GetComponentInChildren<Collider>().enabled = true;
                player.GetComponent<player_movement>().range_slot.transform.GetChild(0).GetComponentInChildren<MeshRenderer>().enabled = true;
                player.GetComponent<player_movement>().range_slot.transform.GetChild(0).GetChild(0).GetComponentInChildren<MeshRenderer>().enabled = false;
                player.GetComponent<player_movement>().range_slot.transform.GetChild(0).transform.parent = null;

                transform.parent.transform.parent = player.GetComponent<player_movement>().range_slot.transform;
                transform.GetComponent<Collider>().enabled = false;
                transform.GetComponent<MeshRenderer>().enabled = false;
                transform.GetComponentInChildren<MeshRenderer>().enabled = true;
                transform.parent.transform.localPosition = Vector3.zero;
            }
        }
    }
}
