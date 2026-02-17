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
        button1.clicked += Click1;//bind function to button click
        if (FindAnyObjectByType<SeedContainer>() != null)
        {
            seedContainer = FindAnyObjectByType<SeedContainer>();
            Destroy(seedContainer.gameObject); //removes seed container
        }
    }

    void Click1()
    {
        SceneManager.LoadScene("Menu"); //loads menu scene
    }

    private void OnDisable()
    {
        button1.clicked -= Click1; //unbinds button function
    }
}
