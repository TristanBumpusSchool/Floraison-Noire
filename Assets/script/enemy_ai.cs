using UnityEngine;
using UnityEngine.AI;

public class enemy_ai : MonoBehaviour
{

    //Diffrent states : 1 = idle/wander 2 = fallow player, 3 = attack
    public int current_state = 1;



    GameObject player;

    //Enemy stats
    [Header("Stats")]
    public float speed = 3;
    public float max_health = 2;
    public hp_system hp_script;
    public float base_damage = 1;
    public damage_system damage_script;

    [Header("Range")]
    public float attack_range = 2;
    public float follow_range = 10;


    /// <summary>
    /// Switchs between the diffrent stats of the enemy
    /// </summary>
    void state_manager()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < attack_range)
        {
            current_state = 3;
        }
        else if (Vector3.Distance(player.transform.position, transform.position) < follow_range)
        {
            current_state = 2;
        }
        else
        {
            current_state = 1;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<NavMeshAgent>().enabled = true;
        player = GameObject.FindWithTag("player");
        print(transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //GetComponent<NavMeshAgent>().enabled = true;
        if (current_state == 2)
        {
            GetComponent<NavMeshAgent>().destination = player.transform.position;
            GetComponent<NavMeshAgent>().speed = speed;
            transform.LookAt(player.transform.position);
        }
        else
        {
            GetComponent<NavMeshAgent>().speed = 0;
        }
        if (current_state == 3)
        {
            GetComponent<Animator>().SetBool("attack", true);
            transform.LookAt(player.transform.position);
        }
        else
        {
            GetComponent<Animator>().SetBool("attack", false);
        }





        state_manager();

    }

    public void end_attack()
    {
        GetComponent<Animator>().SetBool("attack", false);
    }
}
