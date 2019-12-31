using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public enum element
    {
        Fire,
        Water,
        Earth
    }

    delegate void SwapElementEvent();
    public element currentElement;
    SwapElementEvent swapEvent;

    void Awake()
    {
        swapEvent = null;
        // Subscribe in other scripts like this -> swapEvent += new SwapElementEvent(functionName)
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Swaps the element of your base to another type
    void SwapElement(element elem)
    {
        if(elem != currentElement)
        {
            currentElement = elem;
            swapEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
