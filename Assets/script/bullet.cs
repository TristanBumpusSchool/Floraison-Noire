using Unity.VisualScripting;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public float speed;
    public Vector3 direction;
    public string source;

    public int bounces = 0;
    public int homing = 0;

    private bool start = false;

    private GameObject target;
    private GameObject enemy_just_hit;

    void start_colliding()
    {
        start = true;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = direction * speed;
        Invoke("start_colliding", .1f);
        transform.LookAt(GetComponent<Rigidbody>().linearVelocity + transform.position);
        
        float distance = Mathf.Infinity;
        
        if (homing > 0)
        {
            foreach (GameObject e in GameObject.FindGameObjectsWithTag("enemy"))
            {
                RaycastHit ray;
                Physics.queriesHitBackfaces = true;
                Physics.Raycast(transform.position + Vector3.up * 2, (e.transform.position - (transform.position + Vector3.up)),out ray, 1000f);
                
                Debug.DrawRay(transform.position, (e.transform.position - transform.position) * ray.distance, Color.red);
                if (ray.collider != null)
                {
                    print(ray.collider.tag);
                    if (ray.collider.tag == "enemy" || ray.collider.tag == "untagged")
                    {
                        if (Vector3.Distance(e.transform.position, transform.position) < distance)
                        {
                            distance = Vector3.Distance(e.transform.position, transform.position);
                            print(Vector3.Distance(e.transform.position, transform.position));
                            target = e;
                        }
                        
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Rigidbody>().linearVelocity = direction * speed;
        transform.LookAt(GetComponent<Rigidbody>().linearVelocity + transform.position);
        if (homing > 0) {
            float distance = Mathf.Infinity;
            foreach (GameObject e in GameObject.FindGameObjectsWithTag("enemy"))
            {
                RaycastHit ray;
                //RaycastHit ray2;
                Physics.queriesHitBackfaces = true;
                Physics.Raycast(transform.position + Vector3.up * 2, (e.transform.position - (transform.position + Vector3.up * 2)), out ray, 1000f);
                //Physics.Raycast(transform.position + Vector3.up * 2, (e.transform.position - (transform.position + Vector3.up)), out ray, 1000f);

                Debug.DrawRay(transform.position, (e.transform.position - transform.position) * ray.distance, Color.red);
                if (ray.collider != null)
                {
                    print(ray.collider);
                    print(ray.point);
                    if (ray.collider.tag == "enemy" || ray.collider.tag == "untagged")
                    {
                        if (Vector3.Distance(e.transform.position, transform.position) < distance & e != enemy_just_hit)
                        {
                            distance = Vector3.Distance(e.transform.position, transform.position);
                            print(Vector3.Distance(e.transform.position, transform.position));
                            target = e;
                        }

                    }
                }
            }
            if (target != null)
            {
                direction = (target.transform.position - transform.position).normalized;
            }
        }
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        // if (collision.gameObject.tag != source & start)
        //{
        //    if (bounces > 0)
        //    {
        //        Destroy(gameObject);
        //    }
        //    else
        //    {
        //        bounces -= 1;
        //        //direction += direction + transform.right ;
        //        direction = Vector3.Reflect(direction, collision.contacts[0].normal).normalized;
        //    }
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != source & start)
        {
            if (bounces <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                bounces -= 1;
                enemy_just_hit = other.gameObject;
            }
        }
    }
}
