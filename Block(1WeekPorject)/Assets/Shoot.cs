using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    Rigidbody rigid;
    bool isMove = true;
    bool isFloor = false;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    private void Update()
    {

        if (isFloor)
        {
            rigid.AddForce(Vector3.back * 30);

            if (rigid.velocity.z <= 5)
            {
                rigid.AddForce(Vector3.zero * 0);
                rigid.velocity = Vector3.zero;
                rigid.useGravity = true;
                isFloor = false;
            }
        }

        if (isMove)
        {
            if (rigid.velocity.x == 0 && rigid.velocity.y == 0 && rigid.velocity.z == 0 && rigid.useGravity== true)
            {
                ShootManager.Instance.Bolck_Move_Stop(transform);
                isMove = false;
            }
           
        }

    }
    private void FixedUpdate()
    {
       
    }
    public void shoot(float Speed)
    {
        rigid.AddForce(Vector3.forward * Speed);
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        isFloor = true;

    }
    private void OnTriggerEnter(Collider other)
    {
        isFloor = true;
        if (other.tag == "wall")
        {
           
            //rigid.useGravity = true;

        }

    }
}
