using Lumi;
using UnityEngine;
using UnityEngine.AI;

public class enemy_ai : MonoBehaviour
{

    //Diffrent states : 1 = idle/wander 2 = fallow player, 3 = attack, 4 = go to last location
    public int current_state = 1;



    GameObject player;

    //Enemy stats
    [Header("Stats")]
    public float speed = 3;
    public float max_health = 2;
    public hp_system hp_script;
    public float base_damage = 1;
    public damage_system damage_script;
    bool staggered = false;

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
        if (staggered) {
            current_state = 1;
        }
        else if (Vector3.Distance(player.transform.position, transform.position) < attack_range)
        {
            RaycastHit ray;
            Physics.Raycast(transform.position, (player.transform.position - transform.position), out ray, Vector3.Distance(player.transform.position, transform.position));
            //Debug.DrawRay(transform.position + (transform.forward * 1f), (player.transform.position - transform.position) * ray.distance, Color.red, 1f);
            print(ray.collider);
            if (ray.collider.tag == "player")
            {
                current_state = 3;
            }

        }
        else if (Vector3.Distance(player.transform.position, transform.position) < follow_range)
        {
            RaycastHit ray;
            Physics.Raycast(transform.position + (transform.up * 1f), (player.transform.position - transform.position), out ray, Vector3.Distance(player.transform.position, transform.position));
            Debug.DrawRay(transform.position + (transform.up * 1f), (player.transform.position - transform.position) * ray.distance, Color.red, 5f);
            print(ray.collider);
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
        GetComponent<Animator>().SetInteger("state", current_state);

        //GetComponent<NavMeshAgent>().enabled = true;
        if (current_state == 2 & !GetComponent<Animator>().GetBool("attack"))
        {
            player_last_position = player.transform.position;
            if (light_detector.SampledLightAmount > 2f) 
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

    public void end_staggered()
    {
        staggered = false;
    }

    public void stagger()
    {
        staggered = true;
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().Play("stagger");
        Invoke("end_staggered", 1f);
    }
}
