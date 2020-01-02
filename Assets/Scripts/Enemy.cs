using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	EntityElement entityElement;
	Health health;

	int index = 0;
	List<Vector3> path;
	Vector3 offset;

	public float speed;
	public int damages;
    public int loot;

	private void Awake()
	{
		entityElement = GetComponent<EntityElement>();
		health = GetComponent<Health>();
	}

	// Start is called before the first frame update
	void Start()
    {
		offset = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), 0);
		transform.Translate(offset);
    }

    // Update is called once per frame
    void Update()
    {
		if (transform.position != path[index] + offset) {
			transform.position = Vector3.MoveTowards(transform.position, path[index] + offset, speed * Time.deltaTime);
			if (transform.position == path[index] + offset) {
				index++;
				if (index == path.Count) {
					index = 0;
				}
			}
		}
    }

	public void SetElement(Gameplay.Element elem)
	{
		entityElement.Element = elem;
	}

	public void SetPath(List<Vector3> selectedPath)
	{
		path = selectedPath;
	}

	public void ApplyDamages(int damages, Gameplay.Element element)
	{
		health.ApplyDamages(damages, element);
	}
}
