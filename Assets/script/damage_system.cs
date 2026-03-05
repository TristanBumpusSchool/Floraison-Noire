using UnityEngine;

public class damage_system : MonoBehaviour
{


    public float damage = 1;
    public string source = "none";


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
        if (other.GetComponent<hp_system>() != null & other.tag != source){
            other.GetComponent<hp_system>().current_hp -= damage;
        }
    }
}
