using Lumi;
using UnityEngine;
using UnityEngine.AI;

public class boss_ai : MonoBehaviour
{

    //Diffrent states : 1 = idle/wander 2 = fallow player, 3 = attack, 4 = go to last location
    public int current_state = 1;
    int last_attack = 0;


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
    public float follow_range = 100;
    Vector3 player_last_position = Vector3.zero;

    [Header("Lights")]
    public LightDetector light_detector;

    /// <summary>
    /// Switchs between the diffrent stats of the enemy
    /// </summary>
    void state_manager()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < attack_range)
        {
            RaycastHit ray;
            Physics.Raycast(transform.position + (transform.forward * 1f), (player.transform.position - transform.position), out ray, Vector3.Distance(player.transform.position, transform.position));
            if (ray.collider != null)
            {
                if (ray.collider.tag == "player")
                {
                    current_state = 3;
                }
            }

        }
        else if (Vector3.Distance(player.transform.position, transform.position) < follow_range)
        {
            RaycastHit ray;
            Physics.Raycast(transform.position + (transform.forward * 1f), (player.transform.position - transform.position), out ray, Vector3.Distance(player.transform.position, transform.position));
            //Debug.DrawRay(transform.position, (player.transform.position - transform.position) * ray.distance, Color.red, 5f);
            if (ray.collider.tag == "player")
            {
                current_state = 2;
            }
            else
            {
                if(current_state == 2 || current_state == 4)
                {
                    current_state = 4;
                }
                else
                {
                    current_state = 1;
                }
              
            }
        }
        else if(current_state == 2 & Vector3.Distance(player.transform.position, transform.position) > follow_range || current_state == 4)
        {
            current_state = 4;
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

        GetComponent<hp_system>().max_hp = max_health;
        GetComponent<hp_system>().current_hp = max_health;
    }

    private void OnDisable()
    {
        GetComponent<NavMeshAgent>().enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //GetComponent<NavMeshAgent>().enabled = true;
        if (current_state == 2 & GetComponent<Animator>().GetInteger("attack") == 0)
        {
            player_last_position = player.transform.position;
            if (light_detector.SampledLightAmount > .75f)
            { 
                GetComponent<NavMeshAgent>().destination = player.transform.position;
                GetComponent<NavMeshAgent>().speed = speed;
                transform.LookAt(player.transform.position); 
            }
            else
            {
                GetComponent<NavMeshAgent>().destination = player.transform.position;
                GetComponent<NavMeshAgent>().speed = speed/2;
                transform.LookAt(player.transform.position);
            }
        }
        else if(current_state == 4)
        {
            GetComponent<NavMeshAgent>().destination = player_last_position;
            GetComponent<NavMeshAgent>().speed = speed;
            transform.LookAt(player_last_position);
            if(Vector3.Distance(player_last_position, transform.position) < 1) {
                current_state = 1;
            }
        }
        else
        {
            GetComponent<NavMeshAgent>().speed = 0;
        }
        if (current_state == 3)
        {
            if (GetComponent<Animator>().GetInteger("attack") == 0) {
                while (true)
                {
                    GetComponent<Animator>().SetInteger("attack", Random.Range(1, 4));
                    if(last_attack != GetComponent<Animator>().GetInteger("attack"))
                    {
                        break;
                    }
                }
                transform.LookAt(player.transform.position);
            }
        }
        else
        {
            GetComponent<Animator>().SetInteger("attack", 0);
        }





        state_manager();

    }

    public void end_attack()
    {
        GetComponent<Animator>().SetInteger("attack", 0);
    }
}
