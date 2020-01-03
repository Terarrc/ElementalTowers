using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public int health;

    public delegate void GameOverEvent();
    public GameOverEvent gameOverEvent;
    public Gameplay gameplay;
    public CarrierManager carrierManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Enemies");
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1, layerMask);

        foreach (Collider2D hit in hits) {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy == null) {
                continue;
            }

            health -= enemy.damages;

            Destroy(enemy.gameObject);

            if (health <= 0) {
                gameOverEvent?.Invoke();
                Destroy(gameObject);
            }
        }

        layerMask = 1 << LayerMask.NameToLayer("Carrier");
        Collider2D carrierHit = Physics2D.OverlapCircle(transform.position, 1, layerMask);
        if (carrierHit != null) {
            Carrier carrier = carrierHit.GetComponent<Carrier>();
            gameplay.Gems += carrier.gift;
            carrier.gameObject.SetActive(false);
            carrierManager.GenerateCarrierPath();
        }
    }
}
