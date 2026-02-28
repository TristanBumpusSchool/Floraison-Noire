using System;
using UnityEditor.Experimental.GraphView;
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

    public float jump_force = 10;
    public float jump_time = 1;

    public bool on_floor = false;

    int jump_direction = 0;
    Vector2 direction;

    //Player stats
    [Header("Stats")]
    public float max_health = 100;
    public hp_system hp_script;
    public float base_damage = 1;
    public damage_system damage_script;
    public float max_stamina = 100;
    public float reacharge_speed = 1;


    Rigidbody rb;

    //Camera related
    [Header("Camera")]
    public Camera cam;
    public float mouseSensitivity = 2f;
    float cameraVerticalRotation = 0f;

    //Attack related
    [Header("Attack")]
    bool attacking = false;

    public void end_attack()
    {
        attacking = false;
        GetComponent<Animator>().SetBool("attack",attacking);
    }

    void camera_movement()
    {

        //Camera movement from this tutorial (03/02) : https://www.youtube.com/watch?v=5Rq8A4H6Nzw

        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate the Camera around its local X axis

        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -80f, 80f);
        cam.transform.localEulerAngles = Vector3.right * cameraVerticalRotation;


        // Rotate the Player Object and the Camera around its Y axis

        transform.Rotate(Vector3.up * inputX);
    }

    void end_jump()
    {
        jump_direction = 0;
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
        Vector3 move_direction = transform.forward * direction.y + transform.right * direction.x;
        move_direction += new Vector3(0, jump_direction * jump_force, 0);
        rb.linearVelocity = move_direction.normalized * speed * speed_modifiyer;
    }

    void floor_detection()
    {
        RaycastHit ray;
        Physics.Raycast(transform.position, Vector3.down, out ray,1.1f);
        if(ray.collider != null)
        {
            on_floor = true;
        }
        else {
            on_floor = false;
        }
    }

    void update_scripts()
    {
        damage_script.damage = base_damage;
        hp_script.max_hp = max_health;
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        update_scripts();

        floor_detection();

        movement();

        camera_movement();

    }

    public void on_move_input(InputAction.CallbackContext context) {
        direction = context.ReadValue<Vector2>();
    }

    public void on_jump_input(InputAction.CallbackContext context)
    {
        if (on_floor)
        {
            jump_direction = 1;
            Invoke("end_jump", jump_time);
        }
        else
        {
            jump_direction = 0;
        }
    }

    public void on_attack_input(InputAction.CallbackContext context)
    {
        if (!attacking & context.performed)
        {
            attacking = true;
            GetComponent<Animator>().SetBool("attack", attacking);
            Invoke("end_attack", .5f);
        }
    }

    public void on_sprint_dash_input(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            speed = dash_speed;
            speed_modifiyer = 2;
        }
        if (context.canceled)
        {
            speed_modifiyer = 1;
        }
    }
}
