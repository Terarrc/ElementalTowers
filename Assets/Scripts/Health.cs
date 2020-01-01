using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	public int MaxHealth;
	public int CurrentHealth;
	protected EntityElement entityElement;

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
				Destroy(gameObject);
			}
		}


	}
}
