using UnityEngine;
using UnityEngine.SceneManagement;


using UnityEngine.UIElements;
public class EndScreen : MonoBehaviour
{
    Button button1;
    UIDocument document;

    void Awake()
    {
        document = GetComponent<UIDocument>();
        var root = document.rootVisualElement;
        button1 = root.Q<Button>("Menu");
        button1.clicked += Click1;
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
