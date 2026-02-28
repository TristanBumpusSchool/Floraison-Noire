using UnityEngine;

public class enemy_spawner : MonoBehaviour
{

    public GameObject[] enemies;
    public int enemy_top_spawn = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(enemies[enemy_top_spawn],transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
