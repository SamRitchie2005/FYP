using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Compass : MonoBehaviour
{
    UIDocument document;
    [SerializeField] List<GameObject> stars;
    [SerializeField] GameObject player;
    [SerializeField] float angle;
    [SerializeField] GameObject target;
    VisualElement compass;
    
    private void Awake()
    {
        document = GetComponent<UIDocument>();
        var root = document.rootVisualElement;
        compass = root.Q<VisualElement>("Compass");
    }

    private void OnEnable()
    {
        Events.onStarEnable += StarEnable;
    }

    private void OnDisable()
    {
        Events.onStarEnable -= StarEnable;
    }
    private void FixedUpdate()
    {
        if (stars[0] != null)
        {
            if (stars[1] != null)
            {
                if ((stars[0].transform.position - player.transform.position).magnitude < (stars[1].transform.position - player.transform.position).magnitude)
                {
                    target = stars[0];
                }
                else { target = stars[1]; }
            }
            else { target = stars[0]; }
        }
        else { target = stars[1]; }


        if (target != null)
        {

            angle = Vector3.SignedAngle(Vector3.right, (target.transform.position - player.transform.position), Vector3.back);
            compass.style.rotate = new Rotate(new Angle(angle, AngleUnit.Degree));
        }
        
    }

    void StarEnable(GameObject star)
    {
        stars.Add(star);
        target = stars[0];
    }
}
