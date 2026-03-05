using UnityEngine;

public class bullet : MonoBehaviour
{

    public float speed;
    public Vector3 direction;
    public string source;
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
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != source & start)
        {
            print(other.tag);
            Destroy(gameObject);
        }
    }
}
