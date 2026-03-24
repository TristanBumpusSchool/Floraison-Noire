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
            print(GameObject.FindGameObjectsWithTag("enemy"));
            foreach (GameObject e in GameObject.FindGameObjectsWithTag("enemy"))
            {

                print(e);

                RaycastHit ray;
                Physics.Raycast(transform.position, (e.transform.position - transform.position), out ray, Vector3.Distance(e.transform.position, transform.position));
                Debug.DrawRay(transform.position, (e.transform.position - transform.position) * ray.distance, Color.red,5f);
                if (ray.collider != null)
                {
                    print(ray.collider);
                    if (ray.collider.tag == "enemy")
                    {
                        if (Vector3.Distance(e.transform.position, transform.position) < distance)
                        {
                            distance = Vector3.Distance(e.transform.position, transform.position);
                        }
                        target = e;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        print(GameObject.FindGameObjectsWithTag("enemy"));
        GetComponent<Rigidbody>().linearVelocity = direction * speed;
        transform.LookAt(GetComponent<Rigidbody>().linearVelocity + transform.position);
        if (homing > 0 & target != null) { 
            direction = (target.transform.position - transform.position).normalized;
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
            }
        }
    }
}
