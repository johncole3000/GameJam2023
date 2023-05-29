using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    private float moveX;
    private float moveY;
    private bool isPunch;
    private float speed;
    
    
    void Start()
    {
        speed = 5;
        gameObject.tag = "Player";
    }

    
    void Update()
    {
        
        transform.position += new Vector3(moveX,moveY,0) * speed * Time.deltaTime;
        animator.SetFloat("Speed", Mathf.Sqrt( Mathf.Pow(moveX,2) + Mathf.Pow(moveY,2)));
        
    }
    void OnMove(InputValue movementvalue){
        Vector2 moveVector = movementvalue.Get<Vector2>();
        moveX = moveVector.x;
        moveY = moveVector.y;
       
        if(moveVector.x > 0.0f){
            gameObject.transform.rotation = Quaternion.Euler(0,0,0);
        }
        else if(moveVector.x < 0.0f){
            gameObject.transform.rotation = Quaternion.Euler(0,180,0);
        }

    }
    void OnJab(InputValue Jab){
        animator.SetBool("IsPunching", true);
        speed = 0.1f;
    }
    public void EndJab(){
        animator.SetBool("IsPunching", false);
        speed = 5.0f;
    }
}
