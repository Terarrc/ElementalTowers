using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{ 
    public int damage = 0;
    public int speed = 0;
    public EntityElement element;
    public GameObject target;
    private bool isGoing = false;
    int enemyLayer;
    float radius;
    Collider2D col;
    Collider2D enemyCollider;

    private void Awake()
    {
        element = GetComponent<EntityElement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        radius = 0.1f;
        enemyLayer = 1 << LayerMask.NameToLayer("Enemies");
        enemyCollider = target.GetComponent<Collider2D>();     
    }

    // Update is called once per frame
    void Update()
    {
        col = Physics2D.OverlapCircle(transform.position, radius, enemyLayer);       

        if (isGoing)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }

        if (col == enemyCollider)
        {
            Enemy enemy = target.GetComponent<Enemy>();
            if (enemy != null)
                enemy.ApplyDamages(damage, element.Element);

            // Destroy Projectile
            Destroy(gameObject);
        }
    }

    // Move towards the target
    public void GoToTarget()
    {
        if(speed != 0 && damage != 0)
        isGoing = true; 
    }

}
