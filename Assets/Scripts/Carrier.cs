using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : MonoBehaviour
{
    private EntityElement entityElement;

    public float speed;
	public int gift;
    public Vector2 target;


    // Start is called before the first frame update
    void Awake()
    {
        entityElement = GetComponent<EntityElement>();

    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 0;
        switch (entityElement.Element) {
            case Gameplay.Element.Earth:
                layerMask = 1 << LayerMask.NameToLayer("Earth Path");
                break;
            case Gameplay.Element.Fire:
                layerMask = 1 << LayerMask.NameToLayer("Fire Path");
                break;
            case Gameplay.Element.Water:
                layerMask = 1 << LayerMask.NameToLayer("Water Path");
                break;
        }
        if (Physics2D.OverlapCircle(transform.position, 0.2f, layerMask))
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }

}
