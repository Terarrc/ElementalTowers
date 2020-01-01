using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : MonoBehaviour
{
    public EntityElement entityElement;

    public int speed = 0;
    public Vector2 target;
    public BoxCollider2D targetCollider;
    private bool isGoing = true;



    // Start is called before the first frame update
    void Awake()
    {
		entityElement = GetComponent<EntityElement>();

	}

    // Update is called once per frame
    void Update()
    {
        if (isGoing)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
        }
    }

    // Moves Carrier towards the target
    public void GoToTarget()
    {
        if (speed != 0)
        {
            isGoing = true;
        }
    }
}
