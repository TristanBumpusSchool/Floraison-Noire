using UnityEngine;

public class enemy_spawner : MonoBehaviour
{

    public GameObject[] enemies;
    public int enemy_top_spawn = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject e = GameObject.FindWithTag("enemy_manager").GetComponent<enemy_manager>().random_enemy();
        e.transform.SetParent(null);
        e.transform.position = transform.position;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
