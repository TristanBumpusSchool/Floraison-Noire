using Unity.VisualScripting;
using UnityEngine;

public class door : MonoBehaviour
{

    public GameObject visible_door;
    public GameObject enemies_spawners_to_activate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("enemy").Length == 0)
        {
            visible_door.SetActive(false);
        }
        else
        {
            visible_door.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            enemies_spawners_to_activate.SetActive(true);
        }
    }

}
