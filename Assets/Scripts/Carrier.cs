using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : MonoBehaviour
{
	EntityElement entityElement;

    // Start is called before the first frame update
    void Awake()
    {
		entityElement = GetComponent<EntityElement>();

	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
