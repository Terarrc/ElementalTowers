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
        //entityElement = GetComponent<>
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void ApplyDamages(int damages, Gameplay.Element element)
	{
		//if (Gameplay.IsElementStrongAgainst(element, ))
		CurrentHealth -= damages;
	}
}
