using UnityEngine;

public class Star : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Events.OnStar?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
