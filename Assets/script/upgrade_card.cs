using UnityEngine;

public class upgrade_card : MonoBehaviour
{

    public GameObject parent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void upgrade()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Destroy(parent);
    }

}
