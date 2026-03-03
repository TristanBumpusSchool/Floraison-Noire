using UnityEngine;

public class collectable : MonoBehaviour
{

    public GameObject menu_to_create;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("enemy").Length == 0) {
            GetComponent<SphereCollider>().enabled = true;
            GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "player")
        {
            Instantiate(menu_to_create);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            Destroy(gameObject);
        }
    }
}
