using UnityEngine;

public class SeedContainer : MonoBehaviour
{
    public int MainSeed;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

 
}
