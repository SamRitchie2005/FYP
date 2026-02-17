using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
   
    NavMeshAgent agent;
    int Health = 100;
 
    [SerializeField] public GameObject target;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }



    public void Hit(int damage) // handles enemy taking damage and enemy death
    {
        Health-=damage;
        if (Health == 0)
        {
            Destroy(this.gameObject);
        }
    }


    void Update()
    {
        if (target != null) //sets player as a target to move towards if within range
        {
            agent.SetDestination(target.transform.position);
        }
        else { agent.SetDestination(transform.position); }
    }


}
