using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;

public class temp_ending : MonoBehaviour
{

    public GameObject boss;
    public GameObject player;

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
        if(other.tag == "player")
        {
            player.GetComponent<player_movement>().boss = boss.transform.GetChild(3).gameObject;
            player.GetComponent<player_movement>().start_boss_intro();
            
            boss.SetActive(true);
            boss.GetComponentInChildren<Animator>().Play("intro");

            Destroy(gameObject);
        }
    }

}
