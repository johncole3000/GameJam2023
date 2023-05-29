using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   public int Health;

    public Transform attackpos;
    public LayerMask playerlayer;
    public float chaseRadius;
    public float speed;
    public string names;
    public float attackCD;
    private float betweenAttk;
    [SerializeField] private GameObject target;
    public Animator animator;
    void Start()
    {
        attackCD = 3;
        chaseRadius = 2;
        Health = 3;
        speed = 0.5f;
        target = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {




        if(distanceCheck() < 0.3f){  //attackingPhase
            speed = 0.1f;
            if(betweenAttk <= 0){
                attack();

                betweenAttk = attackCD;
            }
            else{
                betweenAttk -= Time.deltaTime;
            }
        }
        else if(distanceCheck() <= chaseRadius ){
            follow(); //movement
            speed = 0.5f;
        }
        else{
            animator.SetFloat("Speed",0);
        }


        if(Health<=0){ //kill when health drop to zero
            Die();
        }
        
    }


    void follow(){
        animator.SetFloat("Speed",1);
        float horizontal = transform.position.x - target.transform.position.x;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position,speed*Time.deltaTime);
        if(horizontal < 0){
            gameObject.transform.rotation = Quaternion.Euler(0,0,0);
        }
        if (horizontal > 0){
            gameObject.transform.rotation = Quaternion.Euler(0,180,0);
        }
    }

    float distanceCheck(){
        float distance = Vector3.Distance(transform.position, target.transform.position);
        return distance;
    }

    void Die(){
        Destroy(this.gameObject);
    }
    void EndJab(){
        animator.SetBool("IsJab",false);
    }
    void attack(){

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackpos.position,0.2f,playerlayer);
        animator.SetBool("IsJab",true);
        for(int i =0; i < enemiesHit.Length; i++){
            enemiesHit[i].GetComponent<PlayerCombat>().Health -= 1;
        
        }
        Debug.Log("Player been hit");
    }
}