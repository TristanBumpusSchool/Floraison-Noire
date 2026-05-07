using Lumi;
using UnityEngine;
using UnityEngine.AI;

public class enemy_ai : MonoBehaviour
{

    //Diffrent states : 0 = staggerd, 1 = idle/wander, 2 = fallow player, 3 = attack, 4 = go to last location, 5 = launching rock
    public int current_state = 1;



    GameObject player;

    //Enemy stats
    [Header("Stats")]
    public float speed = 3;
    public float max_health = 2;
    public hp_system hp_script;
    public float base_damage = 1;
    public damage_system damage_script;
    public bool staggered = false;

    [Header("Range")]
    public float attack_range = 2;
    public float follow_range = 100;
    Vector3 player_last_position = Vector3.zero;

    [Header("Lights")]
    public LightDetector light_detector;

    NavMeshAgent nav_agent;



    /// Switchs between the diffrent stats of the enemy
    void state_manager()
    {
        if (staggered) {
            current_state = 0;
        }
        else if (Vector3.Distance(player.transform.position, transform.position) < attack_range)
        {
            RaycastHit ray;
            Physics.Raycast(transform.position, (player.transform.position - transform.position), out ray, Vector3.Distance(player.transform.position, transform.position));
            //Debug.DrawRay(transform.position + (transform.forward * 1f), (player.transform.position - transform.position) * ray.distance, Color.red, 1f);
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
            Physics.Raycast(transform.position, (player.transform.position - transform.position), out ray, Vector3.Distance(player.transform.position, transform.position));
            //Debug.DrawRay(transform.position + (transform.up * 1f), (player.transform.position - transform.position) * ray.distance, Color.red, 5f);
            if (ray.collider != null)
            {
                if (ray.collider.tag == "player")
                {
                    current_state = 2;
                }
            }
            else
            {
                if (current_state == 2 || current_state == 4)
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

    void OnEnable()
    {
        //GetComponent<NavMeshAgent>().Warp(transform.position);
        player = GameObject.FindWithTag("player");
        staggered = false;
        GetComponent<Collider>().enabled = true;

        GetComponent<hp_system>().max_hp = max_health;
        GetComponent<hp_system>().current_hp = max_health;
        current_state = 1;

        //if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 5.0f, NavMesh.AllAreas))
        //{
        //    // 2. Teleport the Agent's logic to that point
        //    GetComponent<NavMeshAgent>().Warp(hit.position);
        //    GetComponent<NavMeshAgent>().enabled = true;
        //}
    }

    void Start()
    {
        //GetComponent<NavMeshAgent>().enabled = true;
        player = GameObject.FindWithTag("player");

        GetComponent<hp_system>().max_hp = max_health;
        GetComponent<hp_system>().current_hp = max_health;
        nav_agent = GetComponent<NavMeshAgent>();
    }

    private void OnDisable()
    {
        nav_agent.enabled = false;
    }

    private void Update()
    {
        if (!nav_agent.enabled)
        {
            nav_agent.enabled = true;
            nav_agent.Warp(transform.position);
            nav_agent.isStopped = false;
        }
        if (!nav_agent.isOnNavMesh)
        {
            nav_agent.Warp(transform.position);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        state_manager();

        GetComponent<Animator>().SetInteger("state", current_state);


        if (current_state == 2 & !GetComponent<Animator>().GetBool("attack"))
        {
            if (!nav_agent.isActiveAndEnabled || !nav_agent.isOnNavMesh)
            {
                return;
            }
            player_last_position = player.transform.position;
            if (light_detector.SampledLightAmount > 2f) 
            {
                if (nav_agent.isOnNavMesh)
                {
                    nav_agent.isStopped = false;
                    nav_agent.destination = player.transform.position;
                    nav_agent.speed = speed;
                }
            }
            else
            {
                if (nav_agent.isOnNavMesh)
                {
                    nav_agent.isStopped = false;
                    nav_agent.destination = player.transform.position;
                    nav_agent.speed = speed / 2;
                }
            }
        }
        else if(current_state == 4)
        {
            if (!nav_agent.isActiveAndEnabled || !nav_agent.isOnNavMesh)
            {
                return;
            }
            if (nav_agent.isOnNavMesh)
            {
                nav_agent.isStopped = false;
                nav_agent.destination = player_last_position;
                nav_agent.speed = speed;
                if (Vector3.Distance(player_last_position, transform.position) < 1)
                {
                    current_state = 1;
                }
            }
        }
        else
        {
            if (nav_agent.isOnNavMesh)
            {
                nav_agent.isStopped = true;
            }
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





        

    }

    public void end_attack()
    {
        GetComponent<Animator>().SetBool("attack", false);
    }

    public void end_staggered()
    {
        staggered = false;
        current_state = 3;
    }

    public void stagger()
    {
        if (GetComponent<hp_system>().current_hp > 0)
        {
            staggered = true;
            GetComponent<Animator>().SetBool("attack", false);
            GetComponent<Animator>().Play("stagger");
            Invoke("end_staggered", 1f);
        }
    }

    public void death()
    {
        transform.SetParent(GameObject.FindWithTag("enemy_manager").transform.GetChild(0).transform, false);
    }
}
