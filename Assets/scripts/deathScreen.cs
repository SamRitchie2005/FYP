using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deathScreen : MonoBehaviour
{
    int seed;
    SeedContainer seedContainer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (FindAnyObjectByType<SeedContainer>() != null)
        {
            seedContainer = FindAnyObjectByType<SeedContainer>();
            seed = seedContainer.MainSeed;
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
    }
}
