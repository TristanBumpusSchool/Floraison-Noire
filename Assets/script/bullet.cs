using Unity.VisualScripting;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public string source;

    public int bounces = 0;
    public int homing = 0;

    public Rigidbody rb;

    private bool start = false;

    private GameObject target;

    void start_colliding()
    {
        start = true;
    }

    void delete()
    {
        Destroy(gameObject);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Invoke("delete", 10000);

        rb.linearVelocity = direction * speed;
        Invoke("start_colliding", .1f);
        transform.LookAt(rb.linearVelocity + transform.position);
        
        float distance = Mathf.Infinity;
        
        if (homing > 0)
        {
            foreach (GameObject e in GameObject.FindGameObjectsWithTag("enemy"))
            {
                RaycastHit ray;
                Physics.Raycast(transform.position, (e.transform.position - transform.position), out ray, Vector3.Distance(e.transform.position, transform.position));
                //Debug.DrawRay(transform.position, (e.transform.position - transform.position) * ray.distance, Color.red,5f);
                if (ray.collider != null)
                {
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

        rb.linearVelocity = direction * speed;
        transform.LookAt(rb.linearVelocity + transform.position);
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
