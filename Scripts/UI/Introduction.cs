using UnityEngine;
using UnityEngine.EventSystems;

public class Introduction : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    GameObject childObj;
    void Start()
    {
        childObj = transform.GetChild(2).gameObject;
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        //Output the name of the GameObject that is being clicked
        // Debug.Log(name + "Game Object Click in Progress");
        childObj.SetActive(true);
    }

    //Detect if clicks are no longer registering
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        //Debug.Log(name + "No longer being clicked");
        childObj.SetActive(false);
    }
}
