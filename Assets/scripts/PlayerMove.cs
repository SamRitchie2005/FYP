using System.Collections;

using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    GameObject sword;
    BoxCollider2D SwordCollison;
    float speed;
    bool canDash;
    bool canAttack;
    private Vector2 moveInput;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sword = transform.Find("Sword").gameObject;
        SwordCollison = sword.GetComponent<BoxCollider2D>();
        sword.SetActive(false);
        speed = 5f;
        canDash = true;
        canAttack = true;
    }
   
    public void MoveUp(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput*speed;
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (canAttack && context.started)
        {
            StartCoroutine(Attack());
            canAttack = false;
        }
        
       
    }


    public void Dash(InputAction.CallbackContext context)
    {
        //speed = 50f;
        if (canDash && context.started)
        {
            StartCoroutine(Dash());
            canDash = false;
        }
        StartCoroutine(DashCooldown());
    }

    IEnumerator Attack()
    {
        sword.SetActive(true);
        float attackProgress = 0;
        while (attackProgress < 180)
        {
            sword.transform.RotateAround(this.transform.position, Vector3.forward, 2);
            attackProgress++;
            yield return new WaitForSeconds(0.001f);
        }
        sword.SetActive(false);
        StartCoroutine(AttackCooldown());
        yield return null;
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.5f);
        canAttack = true;
        yield return null;
    }

    IEnumerator Dash()
    {
       // if (speed == 5f)
       // {
            speed = 25f;
            yield return new WaitForSeconds(.2f);
     //   }
    //    else {
            speed = 5f;
            yield return null;
     //   }
    }

    IEnumerator DashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(1f);
        canDash = true;
        yield return null;
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Events.OnDamage?.Invoke(1);
        } 
    }
}
