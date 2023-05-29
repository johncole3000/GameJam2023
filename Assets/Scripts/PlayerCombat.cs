using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerCombat : MonoBehaviour
{
    public Collider2D collid;
    public int Health;
    private float betweenatk;
    public Transform attackpos;
    public float attkRange;
    public LayerMask enemylayer;
    public float attkCD;
    public Animator animator;
    public AudioSource sfx;
    public int damage;
    private float times;
    void Start()
    {
        Health = 3;
        attkRange = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(betweenatk >= 0){
            betweenatk -= Time.deltaTime;
        }

        if( Health <= 0){
            times += Time.deltaTime;
            this.gameObject.GetComponent<PlayerMovement>().speed *= 0f;
            damage=0;
            betweenatk = 9999;
            gameObject.transform.rotation = Quaternion.Euler(0,0,90);
            collid.enabled = false;
            if(times >=3){
                
                Invoke("Die",3);
            }
            
            
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

    void Die(){
        Destroy(this.gameObject);
        SceneManager.LoadScene(0);
    }
}
