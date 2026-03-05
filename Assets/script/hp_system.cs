using TMPro;
using UnityEngine;

public class hp_system : MonoBehaviour
{

    public float max_hp = 100;
    public float current_hp;

    public GameObject hp_ui;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current_hp = max_hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (current_hp > max_hp) { 
            current_hp = max_hp;
        }
        if (current_hp <= 0) { 
            Destroy(gameObject);
        }

        if (hp_ui != null) { 
            hp_ui.GetComponent<TextMeshProUGUI>().text = "PV:" + current_hp.ToString() + "/" + max_hp.ToString();
        }

    }
}
