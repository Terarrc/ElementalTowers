using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    public Gameplay gameplay;
    public int delay = 1;
    public int quantity = 1;
    private float timer;

    private EntityElement entityElement;

    void Awake() {
        entityElement = gameObject.GetComponent<EntityElement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = delay;
    }

    // Update is called once per frame
    void Update()
    {
        if (entityElement.IsActive) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                timer = delay;

                gameplay.Gems += quantity;
            }
        } else {
            timer = delay;
        }
    }
}
