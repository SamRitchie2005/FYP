using UnityEngine;
using UnityEngine.SceneManagement;

//using UnityEngine.UI;
using UnityEngine.UIElements;

public class ButtonHandle : MonoBehaviour
{
    Button button1;
    Button button2;
    Button button3;
    [SerializeField] UIDocument document;

    void Start()
    {
        var root = document.rootVisualElement; 
        button1 = root.Q<Button>("Button1");
        button2 = root.Q<Button>("Button2");
        button3 = root.Q<Button>("Button3");

        button1.clicked += Click1;
        button2.clicked += Click2;
        button3.clicked += Click3;
    }

    void Click1()
    {

    }
    void Click2()
    {
        SceneManager.LoadScene("SampleScene");
        
    }
    void Click3()
    {

    }

}
