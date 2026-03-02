using UnityEngine;

public class wall_detection : MonoBehaviour
{

    public bool on_wall = false;

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
