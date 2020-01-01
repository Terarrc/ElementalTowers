using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public int damage;
    public int speed;
    public int fireRate;
    public float range;
    public Projectile projectile;
    public EntityElement entityElement;
    private Enemy target;

    private float timerFire;

    // Search for an ennemy and lock on until he dies or leaves range

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        timerFire = 1 / fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.deltaTime;
        timerFire -= time;

        if(timerFire <= 0)
        {
            Fire();
            timerFire = 1 / fireRate;
        }
    }

    // Get all ennemies inside the range of the turret
    List<GameObject> GetAllEnnemiesInRange()
    {
        List<GameObject> ennemiesInRange = null;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range, LayerMask.NameToLayer("Ennemies"));

        foreach (Collider2D h in hits)
        {
            ennemiesInRange.Add(h.gameObject);
        }

        return ennemiesInRange;
    }

    // Returns the closest ennemy in the defined range that the turret can attack
    GameObject GetClosestEnnemy(List<GameObject> enemies)
    {
        GameObject lockOnTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject potentialTarget in enemies)
        {         
            EntityElement ennemyElement = potentialTarget.GetComponent<EntityElement>();
            if (ennemyElement != null)
            {
                // If the attacking elements is strong agains the defending element
                if (Gameplay.IsElementStrongAgainst(entityElement.Element, ennemyElement.Element))
                {
                    Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
                    float dSqrToTarget = directionToTarget.sqrMagnitude;
                    if (dSqrToTarget < closestDistanceSqr)
                    {
                        closestDistanceSqr = dSqrToTarget;
                        lockOnTarget = potentialTarget;
                    }
                }
            }
        }

        return lockOnTarget;
    }

    // Fires a projectile at a target
    void Fire()
    {
        GameObject target;

        target = GetClosestEnnemy(GetAllEnnemiesInRange());

        Vector2 startPoint = transform.position;
        Vector2 targetPoint = target.transform.position;

        projectile = Instantiate(projectile, startPoint, Quaternion.identity);
        projectile.damage = 1;
        projectile.speed = 1;
        projectile.target = target;
        projectile.GoToTarget();
        projectile.element = entityElement;
    }

}
