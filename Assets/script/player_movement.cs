using System;
using System.Collections.Generic;
using System.Linq;
using Lumi;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class player_movement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //Movement
    [Header("Movement")]
    public float speed = 0;
    public float max_speed = 100;
    public float decceleration = 1;
    public float speed_modifiyer = 1;
    public float dash_speed = 100;
    public float dash_cost = 25;
    Vector2 direction;

    [Header("Jump")]
    //Jump realated
    public float jump_force = 10;
    public float jump_time = 1;
    public float jump_buffer = .1f;
    public float coyote_time = .1f;

    public bool on_floor = false;
    public bool will_jump = false;
    public bool can_coyote_jump = false;

    public wall_detection wall_detecter;

    float jump_direction = 0;
    
    bool can_wall_jump = false;

    bool reached_top_jump = false;

    [Header("Gravity")]
    public float max_gravity;
    public float accel_gravity;
    public float gravity;

    //Player stats
    [Header("Stats")]
    public float max_health = 100;
    public hp_system hp_script;
    public float base_damage = 1;
    public damage_system damage_script;
    public float max_stamina = 100;
    public float stamina = 100;
    public float stamina_regen = 1;
    public TextMeshProUGUI stamina_ui;
    public float reacharge_speed = 1;


    public Rigidbody rb;

    
    //Attack
    [Header("Attack")]
    public GameObject bullet;
    public GameObject attack_box;
    public int weapon_id = 1;
    public bool attacking = false;
    public GameObject melee_slot;
    public GameObject range_slot;

    [Header("Bullet Stats")]
    public TextMeshProUGUI ammo_ui;

    public float reload_speed = 1f;
    public float ammo_max = 5;
    public float ammo_current = 5;
    public float bullet_cost = 1;
    public int bullet_bounce = 0;
    public int bullet_homing = 0;
    public int bullet_on_melee = 0;
    GameObject current_bullet;

    

    [Header("Blocking")]
    public bool blocking = false;
    public int block_cost = 10;
    public bool can_parry = false;
    public float parry_time = 0.1f;

    //Camera related
    [Header("Camera")]
    public Camera cam;
    public float mouse_sensitivity = 2f;
    float cameraVerticalRotation = 0f;
    Vector2 camera_input = Vector2.zero;

    [Header("Light")]
    public LightDetector light_detector;

    [Header("Animations")]
    public List<AnimationClip> animations;

    public Animator anim;
    AnimatorStateInfo state_info;

    //Interactions
    GameObject interaction_object;



    public void end_attack()
    {
        attacking = false;
    }

    void camera_movement()
    {

        //Camera movement from this tutorial (03/02) : https://www.youtube.com/watch?v=5Rq8A4H6Nzw

        // Rotate the Camera around its local X axis

        cameraVerticalRotation -= camera_input.y;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -80f, 80f);
        cam.transform.localEulerAngles = Vector3.right * cameraVerticalRotation;


        // Rotate the Player Object and the Camera around its Y axis

        transform.Rotate(Vector3.up * camera_input.x);
    }

    void end_jump_float()
    {
        //if (can_wall_jump & wall_detecter.on_wall)
        //{
        //    jump();
        //    can_wall_jump = false;
        //}
        reached_top_jump = false;
    }
    
    void end_jump()
    {
        jump_direction = 0;
        //reached_top_jump = true;
        //Invoke("end_jump_float",.05f);
    }

    void end_jump_buffer()
    {
        will_jump = false;
    }

    void end_coyote_time() { 
        can_coyote_jump = false;
    }

    void jump(float force_multiplier = 1) {
        gravity = 1f * jump_force;
        Invoke("end_jump", jump_time);
    }

    void movement()
    {

        if (speed > max_speed) {
            speed -= decceleration;
        }
        else
        {
            speed = max_speed;
        }

        if (!on_floor & jump_direction == 0 & !reached_top_jump)
        {
            gravity += accel_gravity;
            if (gravity < max_gravity)
            {
                gravity = max_gravity;
            }
        }
        //else {
        //    gravity = 0;
        //}

        if (stamina <= 0) { 
            speed_modifiyer = 1;
            stamina = 0;
        }

        //Applies all the movement including jump
        Vector3 move_direction = transform.forward * direction.y + transform.right * direction.x;
        move_direction = move_direction.normalized * speed * speed_modifiyer;
        move_direction += new Vector3(0, gravity, 0);
        rb.linearVelocity = move_direction;
        //rb.AddForce(move_direction * speed * speed_modifiyer, ForceMode.Force);
    }

    void floor_detection()
    {
        RaycastHit ray;
        Physics.Raycast(transform.position, Vector3.down, out ray,1.1f);
        if(ray.collider != null)
        {
            on_floor = true;
            can_coyote_jump = true;
            if (will_jump)
            {
                jump();
                will_jump = false;
                CancelInvoke("end_jump_buffer");
            }
        }
        else {
            on_floor = false;
            if (!IsInvoking("end_coyote_time") & can_coyote_jump)
            {
                Invoke("end_coyote_time", coyote_time);
            }
        }
    }

    void update_scripts()
    {
        int weapon_damage;

        if (weapon_id == 0)
        {
            weapon_damage = melee_slot.GetComponentInChildren<weapon_system>().damage;
        }
        else
        {
            weapon_damage = range_slot.GetComponentInChildren<weapon_system>().damage;
        }
        damage_script.damage = base_damage + weapon_damage;
        hp_script.max_hp = max_health;
        stamina_ui.text = stamina.ToString();
        ammo_ui.text = "Ammo: " + ammo_current.ToString();
    }

    void melee_attack()
    {
        attacking = true;
        attack_box.transform.position = cam.transform.forward * 1.8f + cam.transform.position;
        attack_box.transform.LookAt(cam.transform.forward * 2f + cam.transform.position);
        Invoke("end_attack", .5f);
        if(bullet_on_melee > 0)
        {
            ranged_attack();
        }
    }

    void original_range_attack()
    {
        if (ammo_current >= bullet_cost)
        {
            ammo_current -= bullet_cost;
            GameObject b = Instantiate(bullet);

            b.GetComponent<bullet>().source = "player";
            b.transform.position = cam.transform.position + cam.transform.forward;
            b.GetComponent<bullet>().direction = cam.transform.forward;
            b.GetComponent<bullet>().speed = 50;
            b.GetComponent<damage_system>().source = "player";
            b.GetComponent<bullet>().homing = bullet_homing;
            b.GetComponent<bullet>().bounces = bullet_bounce;
        }
    }

    void ranged_attack() {
        current_bullet.transform.parent = null;
        current_bullet.GetComponent<bullet>().speed = 50;
        current_bullet.GetComponent<bullet>().direction = cam.transform.forward;
    }

    void start_ranged_attack()
    {
        if (ammo_current >= bullet_cost)
        {
            ammo_current -= bullet_cost;
            GameObject b = Instantiate(bullet, range_slot.transform);

            b.transform.localPosition = Vector3.zero;
            b.GetComponent<bullet>().source = "player";
            b.transform.position = cam.transform.position + cam.transform.forward;
            b.GetComponent<bullet>().direction = cam.transform.forward;
            b.GetComponent<bullet>().speed = 0;
            b.GetComponent<damage_system>().source = "player";
            b.GetComponent<bullet>().homing = bullet_homing;
            b.GetComponent<bullet>().bounces = bullet_bounce;
            current_bullet = b;
        }
    }

    void update_stamina()
    {
        if(speed_modifiyer == 2)
        {
            stamina -= 1;
        }
        else if(stamina < max_stamina)
        {
            stamina += stamina_regen;
            if(stamina > max_stamina)
            {
                stamina = max_stamina;
            }
        }
    }

    void block(bool input)
    {
        blocking = input;
        attack_box.transform.position = cam.transform.forward * 1.8f + cam.transform.position;
        attack_box.transform.LookAt(cam.transform.forward * 2f + cam.transform.position);
        attack_box.transform.Rotate(0, 0, -45);
        can_parry = true;
        Invoke("remove_parry", parry_time);
    }

    void remove_parry()
    {
        can_parry = false;
    }

    void ammo_reload()
    {
        if(light_detector.SampledLightAmount > .75)
        {
            ammo_current += 1;
            if(ammo_current > ammo_max)
            {
                ammo_current = ammo_max;
            }
        }
        Invoke("ammo_reload", reload_speed);
    }

    void update_animations()
    {
        if (blocking) {
            if(!anim.GetCurrentAnimatorStateInfo(0).IsName("blocking"))
            {
                anim.Play(animations[0].name);
            }
        }
        else if (attacking)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName(melee_slot.GetComponentInChildren<weapon_system>().weapon_animation.name))
            {
                print("S");
                anim.Play(melee_slot.GetComponentInChildren<weapon_system>().weapon_animation.name);
            }
        }
        else
        {
            
            anim.Play(animations[2].name); 
        }
    
        if(weapon_id == 2)
        {
            range_slot.transform.position = cam.transform.forward * .5f + cam.transform.position + cam.transform.right * .5f;
            range_slot.transform.LookAt(cam.transform.forward * 2f + cam.transform.position);
            //range_slot.transform.Rotate(0, 0, -45);
        }
    }

    void check_for_interactions()
    {
        RaycastHit ray;
        Physics.Raycast(cam.transform.position, cam.transform.forward, out ray, 1000);
        Debug.DrawRay(cam.transform.position, cam.transform.forward * ray.distance, Color.red);
        interaction_object = null;
        if (ray.collider != null)
        {
            if (ray.collider.GetComponent<interactble>() != null)
            {
                interaction_object = ray.collider.gameObject;
            }
        }
    }



    void Start()
    {
        Light[] lights = GameObject.FindGameObjectWithTag("lights").GetComponentsInChildren<Light>();
        light_detector.lights = lights.ToList();

        state_info = anim.GetCurrentAnimatorStateInfo(0);
        Cursor.lockState = CursorLockMode.Locked;

        InvokeRepeating("update_stamina", .1f, .1f);
        Invoke("ammo_reload", reload_speed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        print(light_detector.SampledLightAmount);

        check_for_interactions();

        update_scripts();

        floor_detection();

        movement();

        camera_movement();

        update_animations();

        if (blocking) {
            attack_box.transform.position = cam.transform.forward * 1.8f + cam.transform.position;
            attack_box.transform.LookAt(cam.transform.forward * 2f + cam.transform.position);
            attack_box.transform.Rotate(0, 0, -45);
        }

    }



    //Public functions

    public void parry()
    {
        if (can_parry) {
            block(false);
            melee_attack();
        }
    }

    public void on_move_input(InputAction.CallbackContext context) {
        direction = context.ReadValue<Vector2>();
    }

    public void on_jump_input(InputAction.CallbackContext context)
    {
        if (on_floor & context.performed)
        {
            jump();
            can_wall_jump = true;
            can_coyote_jump = false;
        }
        else if (context.performed & can_coyote_jump)
        {
            jump();
            can_coyote_jump = false;
            CancelInvoke("end_coyote_time");
        }
        else if (context.performed)
        {
            will_jump = true;
            Invoke("end_jump_buffer",jump_buffer);
        }
        else if(context.canceled) 
        {
            jump_direction = 0;
            CancelInvoke("end_jump");
            reached_top_jump = true;
            Invoke("end_jump_float", .05f);
        }

        if (!on_floor & context.performed & can_wall_jump & wall_detecter.on_wall & gravity <= 0) { 
            jump();
            can_wall_jump = false;
        }

    }

    public void on_attack_input(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 1)
        {
            if (weapon_id == 1)
            {
                if (!attacking & context.performed)
                {
                    melee_attack();
                }
            }

            if (weapon_id == 2)
            {
                if (!attacking & context.performed)
                {
                    start_ranged_attack();
                }
                if (!attacking & context.canceled)
                {
                    ranged_attack();
                }
            }
        }
    }

    public void on_sprint_dash_input(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (stamina >= dash_cost)
            {
                speed = dash_speed;
                stamina -= dash_cost;
                speed_modifiyer = 2;
            }
        }
        if (context.canceled)
        {
            speed_modifiyer = 1;
        }
    }

    public void on_block_input(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 1)
        {
            if (context.performed)
            {
                block(true);
            }
            if (context.canceled)
            {
                block(false);
                can_parry = false;
                CancelInvoke("remove_parry");
            }
        }
    }

    public void on_switch_input(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            weapon_id += 1;
            if (weapon_id >= 3)
            {
                weapon_id = 1;
            }
        }
    }

    public void on_camera_input(InputAction.CallbackContext context)
    {
        camera_input = context.ReadValue<Vector2>() * mouse_sensitivity;
    }

    public void on_interaction_input(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(interaction_object != null)
            {
                interaction_object.GetComponent<interactble>().activate(gameObject);
            }
        }
    }
}
