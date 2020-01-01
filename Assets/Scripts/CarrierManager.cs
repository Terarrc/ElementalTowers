using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

public class CarrierManager : MonoBehaviour
{

    public EntityElement entityElement;
    public int speed;
    public Carrier carrier;
    public Gameplay gameplay;
    private GameObject target;
    private GameObject path;

    Vector2 startPos;
    Vector2 tempPos;
    Vector2 scaleSize;

    // Start is called before the first frame update
    void Start()
    {
        // Starting in X seconds event will repeat all x seconds 
        // InvokeRepeating("GenerateCarrier", 2.0f, 0.3f);
        /*
        var list = Enumerable.Range(1, 3).ToList();
        Gameplay.Shuffle(list);

        foreach (var i in list)
        {
            Debug.Log(i);
        }
        */
        GenerateCarrierPath();

    }

    // Update is called once per frame
    void Update()
    {

        //scaleSize = transform.localScale;
        //scaleSize.y = 3f;
        //transform.localScale = scaleSize;
       
    }


    // Create a Gem-Carrier
    void GenerateCarrier(GameObject target)
    {
        Vector2 startPoint = transform.position;
        Vector2 targetPoint = target.transform.position;

        Carrier newCarrier = Instantiate(carrier, startPoint, Quaternion.identity);

        newCarrier.speed = 1;
        newCarrier.target = new Vector2(target.transform.position.x, target.transform.position.y);

        newCarrier.targetCollider = target.GetComponent<BoxCollider2D>();
        newCarrier.GoToTarget();
        
    }

    // Create a Randomized Path

    void GenerateCarrierPath()
    {
        //Vector2 startPath = transform.position;
        int pathLength = 9;
        int minPathLength = 1;
        int remainingElements = 3;
        int remainingLength = pathLength;

        //Path newPath = Instantiate(path, startPoint, Quaternion.identity);


        var list = Enumerable.Range(0, 3).ToList();
        Gameplay.Shuffle(list);

        //foreach (var i in list)
        for (int i = 0; i < list.Count() - 1; i++)
        {
            remainingElements--;
            GameObject tempChild = transform.GetChild(list[i]).gameObject;

            int length = Random.Range(minPathLength, remainingLength - remainingElements * minPathLength);
            Vector3 pos = tempChild.transform.localPosition;
            pos.y = (pathLength - remainingLength) * -1;
            tempChild.transform.localPosition = pos;
            Vector3 scale = tempChild.transform.localScale;
            scale.y = length;
            tempChild.transform.localScale = scale;
            remainingLength = remainingLength - length;
        }

        GameObject child = this.gameObject.transform.GetChild(list[list.Count()-1]).gameObject;

        Vector3 pos2 = child.transform.localPosition;
        pos2.y = (pathLength - remainingLength) * -1;
        child.transform.localPosition = pos2;
        Vector3 scale2 = child.transform.localScale;
        scale2.y = remainingLength;
        child.transform.localScale = scale2;

    }

}
