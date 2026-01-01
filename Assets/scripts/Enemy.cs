using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    NavMeshAgent agent;
    //CircleCollider2D circleCollider;
    [SerializeField] GameObject target;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //circleCollider = GetComponent<CircleCollider2D>();
        
    }


    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.transform.position);
        }
        else { agent.SetDestination(transform.position); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target = null;
        }
    }
}
