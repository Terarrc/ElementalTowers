using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : Health
{
    public SpriteRenderer fireSprite;
    public SpriteRenderer earthSprite;
    public SpriteRenderer waterSprite;

    int earthPhase;
    int firePhase;

    // Start is called before the first frame update
    new void Awake()
    {
        base.Awake();
        earthPhase = MaxHealth * 2 / 3;
        firePhase = MaxHealth / 3;

        entityElement.Element = Gameplay.Element.Water;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ApplyDamages(int damages, Gameplay.Element element)
    {
        int oldHealth = CurrentHealth;
        base.ApplyDamages(damages, element);

        if (oldHealth > earthPhase && CurrentHealth <= earthPhase) {
            entityElement.Element = Gameplay.Element.Earth;
            waterSprite.enabled = false;
        }

        if (oldHealth > firePhase && CurrentHealth <= firePhase) {
            entityElement.Element = Gameplay.Element.Fire;
            earthSprite.enabled = false;
        }
    }
}
