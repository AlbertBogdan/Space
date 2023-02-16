using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public CharacterController controller;
    Vector3 velocity;
    public Animator playerAnim;
    public Rigidbody playerRigid;
    public float w_speed, wb_speed, olw_speed, rn_speed, ro_speed;
    public bool walking;
    bool control = false;
    public Transform playerTrans;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        if (Input.GetKey(KeyCode.LeftShift))
        { controller.Move(move * (10 * 4) * Time.deltaTime); }
        else
        { controller.Move(move * 4 * Time.deltaTime); }
        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(4 * -0.5f * -9.81f);
        }
        velocity.y += -9.81f * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl)) { control = !control; 
            if (control == false) { playerAnim.SetTrigger("Jog"); playerAnim.ResetTrigger("Walk"); }
            else { playerAnim.SetTrigger("Walk"); playerAnim.ResetTrigger("Jog"); }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (control == false) { playerAnim.SetTrigger("Jog"); }
            else { playerAnim.SetTrigger("Walk"); }
            playerAnim.ResetTrigger("Idle");
            walking = true;
            //steps1.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            if (control == false) { playerAnim.ResetTrigger("Jog"); }
            else { playerAnim.ResetTrigger("Walk"); }
            playerAnim.SetTrigger("Idle");
            walking = false;
            //steps1.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerAnim.SetTrigger("JogBack");
            playerAnim.ResetTrigger("Idle");
            //steps1.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnim.ResetTrigger("JogBack");
            playerAnim.SetTrigger("Idle");
            //steps1.SetActive(false);
        }
        //if (Input.GetKey(KeyCode.A))
        //{
        //    playerTrans.Rotate(0, -ro_speed * Time.deltaTime, 0);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    playerTrans.Rotate(0, ro_speed * Time.deltaTime, 0);
        //}
        if (walking == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                //steps1.SetActive(false);
                //steps2.SetActive(true);
                w_speed = w_speed + rn_speed;
                playerAnim.SetTrigger("Sprint");
                playerAnim.ResetTrigger("Jog");
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                //steps1.SetActive(true);
                //steps2.SetActive(false);
                w_speed = olw_speed;
                playerAnim.ResetTrigger("Sprint");
                playerAnim.SetTrigger("Jog");
            }
        }
    }
}