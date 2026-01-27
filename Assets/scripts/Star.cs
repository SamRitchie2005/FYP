using Unity.VisualScripting;
using UnityEngine;

public class Star : MonoBehaviour
{
    private void OnEnable()
    {;
        Events.onStarEnable?.Invoke(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Events.OnStar?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
