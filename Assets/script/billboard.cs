using UnityEngine;
using UnityEngine.UIElements;

public class billboard : MonoBehaviour
{
    public Vector3 extra_rotation = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.down);
        transform.Rotate(extra_rotation);
    }
}
