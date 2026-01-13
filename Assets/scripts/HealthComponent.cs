using Mono.Cecil.Cil;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] UIDocument UI;
    ProgressBar hpBar;
    [SerializeField] int Hp;
    

    void Awake()
    {
        var root = UI.rootVisualElement;
        hpBar = root.Q<ProgressBar>();
        var Bar = root.Q(className: "unity-progress-bar__progress");
        Bar.style.backgroundColor = Color.red;
    }

    private void OnEnable()
    {
        Events.OnDamage += Damage;
    }
    private void OnDisable()
    {
        Events.OnDamage -= Damage;
    }

    void Update()
    {
        hpBar.value = Hp;
    }

    void Damage(int damage)
    {
        Hp = Hp - damage;
    }
}
