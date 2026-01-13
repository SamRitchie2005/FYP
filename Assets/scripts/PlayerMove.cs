using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    float speed;
    bool canDash;
    private Vector2 moveInput;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 5f;
        canDash = true;
    }
   
    public void MoveUp(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput*speed;
    }

    public void Dash(InputAction.CallbackContext context)
    {
        //speed = 50f;
        if (canDash&&context.started)
        {
            StartCoroutine(Dash());
            canDash = false;
        }
        StartCoroutine(DashCooldown());
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
}
