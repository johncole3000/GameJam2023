using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public int Health;
    private float betweenatk;
    public Transform attackpos;
    public float attkRange;
    public LayerMask enemylayer;
    public float attkCD;
    public Animator animator;
    public AudioSource sfx;
    public int damage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(betweenatk >= 0){
            betweenatk -= Time.deltaTime;
        }
    }

    void OnJab(InputValue Jab){
        if(betweenatk <= 0){
            this.gameObject.GetComponent<PlayerMovement>().speed /= 10f;
            betweenatk = attkCD;
            animator.SetBool("IsPunching", true);
            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackpos.position,attkRange,enemylayer);
            for(int i =0; i < enemiesHit.Length; i++){
            enemiesHit[i].GetComponent<Enemy>().Health -= damage;
            sfx.Play();
            }
        }
    }


    public void EndJab(){
        animator.SetBool("IsPunching", false);
        this.gameObject.GetComponent<PlayerMovement>().speed *= 10f;
    }
}
