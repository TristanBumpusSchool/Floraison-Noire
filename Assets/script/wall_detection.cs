using UnityEngine;

public class wall_detection : MonoBehaviour
{

    public bool on_wall = false;
    public Vector3 collision_point = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "wall")
        {
            collision_point = other.ClosestPoint(transform.position);
            on_wall = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "wall")
        {
            on_wall = false;
        }
    }
}
