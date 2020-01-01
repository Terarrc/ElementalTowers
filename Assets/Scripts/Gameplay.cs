﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    public enum Element
    {
        Fire,
        Water,
        Earth
    }

	public Text gemsText;

	public float timerWave;

    public delegate void SwapElementEvent(Element elem);
    public Element PlayerElement;

	private int gems;
    public int Gems {
		get {
			return gems;
		}
		set {
			gems = value;
			gemsText.text = value.ToString();
		}
	}
    public SwapElementEvent swapEvent;

	public delegate void TimerWaveEvent();
	public TimerWaveEvent timerWaveEvent;

	void Awake()
    {
        swapEvent = null;
        PlayerElement = Element.Fire;
        // Subscribe in other scripts like this -> swapEvent += new SwapElementEvent(functionName)
    }

    // Start is called before the first frame update
    void Start()
    {
		InvokeRepeating("NewWave", timerWave, timerWave);
		Gems = 200;
	}

	public void NewWave()
	{
		timerWaveEvent.Invoke();
	}

    // True if the attacking element is strong against the defending element
    public static bool IsElementStrongAgainst(Element elemAttack, Element elemDefense)
    {
        if(elemAttack == Element.Fire && elemDefense == Element.Earth) { return true; }
        if (elemAttack == Element.Water && elemDefense == Element.Fire) { return true; }
        if (elemAttack == Element.Earth && elemDefense == Element.Water) { return true; }
        return false;
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
        if(elem != PlayerElement)
        {
            PlayerElement = elem;
            swapEvent?.Invoke(elem);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
