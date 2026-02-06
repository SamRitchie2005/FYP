using UnityEngine;
using UnityEngine.SceneManagement;


using UnityEngine.UIElements;
public class EndScreen : MonoBehaviour
{
    Button button1;
    UIDocument document;

    SeedContainer seedContainer;

    void Awake()
    {
        document = GetComponent<UIDocument>();
        var root = document.rootVisualElement;
        button1 = root.Q<Button>("Menu");
        button1.clicked += Click1;
        if (FindAnyObjectByType<SeedContainer>() != null)
        {
            seedContainer = FindAnyObjectByType<SeedContainer>();
            Destroy(seedContainer.gameObject);
        }
    }

    void Click1()
    {
        SceneManager.LoadScene("Menu");
    }

    private void OnDisable()
    {
        button1.clicked -= Click1;
    }
}
