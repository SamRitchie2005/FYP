using UnityEngine;

public class Saw : MonoBehaviour
{
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        float turnSpeed = 765f;
        rb.MoveRotation(rb.rotation + turnSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Events.OnDamage?.Invoke(1);
        }
    }
}
