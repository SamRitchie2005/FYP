using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deathScreen : MonoBehaviour
{
    int seed;
    SeedContainer seedContainer;
   
    void Start()
    {
        if (FindAnyObjectByType<SeedContainer>() != null)
        {
            seedContainer = FindAnyObjectByType<SeedContainer>();
            seed = seedContainer.MainSeed; //gets seed from seed container
        }
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(2f);
        if (seed == -1)
        {
            SceneManager.LoadScene("PreBuilt");
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
        }
    } // waits 2 seconds and reloads scene
}
