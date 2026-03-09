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
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().linearVelocity = direction * speed;
        transform.LookAt(GetComponent<Rigidbody>().linearVelocity + transform.position);
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
            if (bounces > 0)
            {
                Destroy(gameObject);
            }
            else
            {
                bounces -= 1;
                //direction += direction + transform.right ;
                direction = Vector3.Reflect(direction, other.ClosestPoint(transform.position)).normalized;
            }
        }
    }
}
