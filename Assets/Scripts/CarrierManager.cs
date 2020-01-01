using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierManager : MonoBehaviour
{

    public EntityElement entityElement;
    public int speed;
    public Carrier carrier;
    public Gameplay gameplay;
    private GameObject target;


    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {

    }


    // Create a Gem-Carrier
    void GenerateCarrier(GameObject target)
    {
        Vector2 startPoint = transform.position;
        Vector2 targetPoint = target.transform.position;

        carrier = Instantiate(carrier, startPoint, Quaternion.identity);
        carrier.speed = 1;
        carrier.target = new Vector2(target.transform.position.x, target.transform.position.y);

        carrier.targetCollider = target.GetComponent<BoxCollider2D>();
        carrier.GoToTarget();
        
    }

    // Create a Randomized Path


}
