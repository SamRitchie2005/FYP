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
        compass = root.Q<VisualElement>("Compass"); //assigns compass visual element
        Events.onStarEnable += StarEnable; //binds event to function
    }


    private void OnDisable()
    {
        Events.onStarEnable -= StarEnable; //unbinds event to function
    }
    private void FixedUpdate()
    {
        if (stars.Count != 0)
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
        } //sets the target as the closest star in the target list

        if (target != null)
        {

            angle = Vector3.SignedAngle(Vector3.right, (target.transform.position - player.transform.position), Vector3.back);
            compass.style.rotate = new Rotate(new Angle(angle, AngleUnit.Degree));
        }         //points compass visual element towards target
    }

    void StarEnable(GameObject star)
    {
        stars.Add(star);
        target = stars[0]; //sets up compass targets in a list
    }
}
