using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class hp_system : MonoBehaviour
{

    public float max_hp = 100;
    public float current_hp;

    public GameObject hp_ui;
    public GameObject hit_effect;
    public AudioClip sound_to_play_on_hit;

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
            if (gameObject.tag == "enemy") {
                gameObject.GetComponent<enemy_ai>().staggered = true;
                gameObject.GetComponent<Collider>().enabled = false;
                gameObject.GetComponent<Animator>().Play("death");
            }
            if (gameObject.tag == "player")
            {
                SceneManager.LoadScene(2);
            }
        }


        if (hp_ui != null) {
            if (hp_ui.GetComponent<Slider>().maxValue != max_hp)
            {
                hp_ui.GetComponent<Slider>().maxValue = max_hp;
            }
            hp_ui.GetComponent<Slider>().value = current_hp;
            
        }

    }
}
