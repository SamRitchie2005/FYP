using UnityEngine;
using UnityEngine.SceneManagement;

//using UnityEngine.UI;
using UnityEngine.UIElements;

public class ButtonHandle : MonoBehaviour
{
    Button button1;
    Button button2;
    Button button3;
    TextField seedbox;
    int seedboxInt;
    [SerializeField] UIDocument document;
    [SerializeField] SeedContainer seed;

    void OnEnable()
    {
        var root = document.rootVisualElement; 
        //button1 = root.Q<Button>("Button1");
        button2 = root.Q<Button>("Button2");
        button3 = root.Q<Button>("Button3");
        seedbox = root.Q<TextField>();
        //button1.clicked += Click1;
        button2.clicked += Click2;
        button3.clicked += Click3; //assigns functions to buttons in menu ui
    }

    private void OnDisable()
    {
        //button1.clicked -= Click1;
        button2.clicked -= Click2;
        button3.clicked -= Click3; //unassigns functions to buttons in menu ui
    }

    void Click1()
    {
        seed.MainSeed = -1;
        SceneManager.LoadScene("PreBuilt"); //loads the prebuilt level and nullifies the seed in the seed continer
    }
    void Click2()
    {
        seed.MainSeed = Random.Range(0, 1000000);
        SceneManager.LoadScene("ProcGen"); //loads the procedurally generated level and sets a random seed in the seed container
       
    }
    void Click3()
    {
        if (int.TryParse(seedbox.value, out seedboxInt))
        {
            seed.MainSeed = Mathf.Abs(seedboxInt);
        }
        else { seed.MainSeed = 1; }
        SceneManager.LoadScene("ProcGen"); ////loads the procedurally generated level and sets the seed in the seed container to a player inputted seed
    }

}
