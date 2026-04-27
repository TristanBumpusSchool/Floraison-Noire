using Unity.VisualScripting;
using UnityEngine;

public class door : MonoBehaviour
{

    public GameObject visible_door;
    public GameObject enemies_spawners_to_activate;
    public AudioClip music_change;
    bool open = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("enemy").Length == 0 & !open)
        {
            visible_door.GetComponent<Collider>().enabled = false;
            visible_door.GetComponent<Animator>().SetFloat("move_speed", 1);
            visible_door.GetComponent<Animator>().Play("fall",0,0);
            open = true;
        }
        else if(open & GameObject.FindGameObjectsWithTag("enemy").Length > 0)
        {
            visible_door.GetComponent<Collider>().enabled = true;
            visible_door.GetComponent<Animator>().SetFloat("move_speed", -1);
            visible_door.GetComponent<Animator>().Play("fall",0,1f);
            open = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "player")
        {
            enemies_spawners_to_activate.SetActive(true);
            if(music_change != null)
            {
                other.gameObject.GetComponentInChildren<AudioSource>().clip = music_change;
            }
        }
    }

}
