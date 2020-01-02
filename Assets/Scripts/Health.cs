using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	public int MaxHealth;
	protected int CurrentHealth;
	protected EntityElement entityElement;

	public delegate void KilledEvent();
	public KilledEvent killedEvent;


    // Start is called before the first frame update
    void Awake()
    {
		entityElement = GetComponent<EntityElement>();
		CurrentHealth = MaxHealth;
	}

    // Update is called once per frame
    void Update()
    {

	}

	public void ApplyDamages(int damages, Gameplay.Element element)
	{
		if (Gameplay.IsElementStrongAgainst(element, entityElement.Element)) {
			CurrentHealth -= damages;	
			if (CurrentHealth <= 0) {
				killedEvent?.Invoke();
				Destroy(gameObject);
			}
		}


	}
}
