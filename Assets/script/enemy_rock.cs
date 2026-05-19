using UnityEngine;

public class enemy_rock : MonoBehaviour
{

    public Transform fallow;
    public Vector3 direction;
    public Vector3 target;
    public float speed = 1;
    public bool held = true;
    public string source = "enemy";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("end_hold", .6f);
        GetComponent<damage_system>().source = source;
    }



    // Update is called once per frame
    void Update()
    {
        if (held)
        {
            transform.position = fallow.position;
        }
        else
        {
            transform.position += direction * speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != source)
        {
            Destroy(gameObject);
        }
    }




    void end_hold()
    {
        held = false;
        direction = (target - transform.position).normalized;
    }



}
