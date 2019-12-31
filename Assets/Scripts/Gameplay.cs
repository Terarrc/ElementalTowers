using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public enum Element
    {
        Fire,
        Water,
        Earth
    }

    delegate void SwapElementEvent();
    public Element currentElement;
    SwapElementEvent swapEvent;

    void Awake()
    {
        swapEvent = null;
        currentElement = Element.Fire;
        // Subscribe in other scripts like this -> swapEvent += new SwapElementEvent(functionName)
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void SwapElementOnClick(int elem)
    {
        switch (elem)
        {
            case 0:
                SwapElement(Element.Fire);
                break;
            case 1:
                SwapElement(Element.Water);
                break;
            case 2:
                SwapElement(Element.Earth);
                break;
        }
    }

    // Swaps the element of your base to another type
    void SwapElement(Element elem)
    {
        Debug.Log(elem);
        if(elem != currentElement)
        {
            currentElement = elem;
            swapEvent?.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
