using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityElement : MonoBehaviour
{
	public bool CanChangeElement;

	private Gameplay.Element element;
	public Gameplay.Element Element { 
		get {
			return element;
		}
		set {
			element = value;

			if (animator != null) {
				switch (value) {
					case Gameplay.Element.Fire:
						animator.SetTrigger("Fire");
						break;
					case Gameplay.Element.Earth:
						animator.SetTrigger("Earth");
						break;
					case Gameplay.Element.Water:
						animator.SetTrigger("Water");
						break;
				}
			}
		}
	}

	private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	// Start is called before the first frame update
	void Start()
    {

    }

	void ChangeElement(Gameplay.Element elem)
	{
		Element = elem;
	}

	public void EnableSwapElement(Gameplay gameplay)
	{
		gameplay.swapEvent += ChangeElement;
	}
}
