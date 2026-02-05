
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] UIDocument UI;
    ProgressBar hpBar;
    Label stars;
    int starNum = 0;
    [SerializeField] int Hp;
    

    void Awake()
    {
        var root = UI.rootVisualElement;
        hpBar = root.Q<ProgressBar>();
        stars = root.Q<Label>("starLabel");
        var Bar = root.Q(className: "unity-progress-bar__progress");
        Bar.style.backgroundColor = Color.red;
        stars.text = starNum.ToString() + "/2 stars collected";
    }

    private void OnEnable()
    {
        Events.OnDamage += Damage;
        Events.OnStar += StarAdd;
    }
    private void OnDisable()
    {
        Events.OnDamage -= Damage;
        Events.OnStar -= StarAdd;
    }

    void Update()
    {
        hpBar.value = Hp;
    }

    void Damage(int damage)
    {
        Hp = Hp - damage;
        if (Hp <= 0)
        {
            SceneManager.LoadScene("death");
        }
    }

    void StarAdd()
    {
        starNum++;
        stars.text = starNum.ToString() + "/2 stars collected";
        if (starNum >= 2) { SceneManager.LoadScene("end"); }
    }
}
