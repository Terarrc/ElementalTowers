using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityElement : MonoBehaviour
{
	public bool CanChangeElement;

    private bool isActive;
    public bool IsActive {
        get {
            return CanChangeElement || isActive;
        }
        set {
            isActive = value;
        }
    }

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
        if (CanChangeElement) {
            Element = elem;
        } else {
            isActive = Element == elem;
            animator.SetBool("isActive", isActive);
        }
	}

	public void EnableSwapElement(Gameplay gameplay)
	{
		gameplay.swapEvent += ChangeElement;
	}
}
