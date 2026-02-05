using UnityEngine;


public class enemyTarget : MonoBehaviour
{

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.parent.gameObject.GetComponent<Enemy>().target = collision.gameObject;
            //this.gameObject.GetComponentInParent(Enemy enemy).target = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.parent.gameObject.GetComponent<Enemy>().target = null;
        }
    }
}
