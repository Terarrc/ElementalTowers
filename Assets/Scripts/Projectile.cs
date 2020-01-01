using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{ 
    public int damage = 0;
    public int speed = 0;
    public Vector2 target;
    public EntityElement element;
    public BoxCollider2D targetCollider;
    private bool isGoing = false;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGoing)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
        }
    }

    // Move towards the target
    public void GoToTarget()
    {
        if(speed != 0 && damage != 0)
        isGoing = true;  
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If collides with the target
        if(collision.collider == targetCollider)
        {
            Enemy enemy = targetCollider.gameObject.GetComponent<Enemy>();
            if(enemy != null)
            enemy.ApplyDamages(damage, element.Element);
        }
    }
}
