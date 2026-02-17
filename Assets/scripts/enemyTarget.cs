using UnityEngine;


public class enemyTarget : MonoBehaviour
{

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")//sets player as target if within distance of the enemy
        {
            transform.parent.gameObject.GetComponent<Enemy>().target = collision.gameObject;
          
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") //removes player as target if out of range
        {
            transform.parent.gameObject.GetComponent<Enemy>().target = null;
        }
    }
}
