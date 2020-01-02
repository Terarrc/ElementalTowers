using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WavesManager : MonoBehaviour
{
    public List<Vector3> Path1;
    public List<Vector3> Path2;

    public Gameplay gameplay;

    public Enemy Blob;

    public int waveLevel;
    public Gameplay.Element element;
    public int additionnalFire;
    public int additionnalEarth;
    public int additionnalWater;

    public Text wavePreview;
    private Queue<Gameplay.Element> nextWaves = new Queue<Gameplay.Element>();

    // Start is called before the first frame update
    void Start()
    {
        gameplay.timerWaveEvent += GenerateWave;
        gameplay.swapEvent += SwapElement;

        switch (gameplay.PlayerElement) {
            case Gameplay.Element.Fire:
                nextWaves.Enqueue(Gameplay.Element.Earth);
                break;
            case Gameplay.Element.Earth:
                nextWaves.Enqueue(Gameplay.Element.Water);
                break;
            case Gameplay.Element.Water:
                nextWaves.Enqueue(Gameplay.Element.Fire);
                break;
        }

        for (int i = 0; i < 7; i++) {
            switch (Random.Range(0, 3)) {
                case 0:
                    nextWaves.Enqueue(Gameplay.Element.Fire);
                    break;
                case 1:
                    nextWaves.Enqueue(Gameplay.Element.Earth);
                    break;
                case 2:
                    nextWaves.Enqueue(Gameplay.Element.Water);
                    break;
            }
        }

        UpdateTextWaves();
    }

    void UpdateTextWaves()
    {
        string message = "";
        int i = 0;
        foreach (Gameplay.Element elem in nextWaves) {
            switch (elem) {
                case Gameplay.Element.Fire:
                    message += "<color=red>O</color>";
                    break;
                case Gameplay.Element.Earth:
                    message += "<color=green>O</color>";
                    break;
                case Gameplay.Element.Water:
                    message += "<color=blue>O</color>";
                    break;
            }
            if (i < nextWaves.Count - 1)
                message += " - ";
            i++;
        }
        wavePreview.text = message;
    }

    void SwapElement(Gameplay.Element elem)
    {
        switch (elem) {
            case Gameplay.Element.Fire:
                additionnalFire++;
                break;
            case Gameplay.Element.Earth:
                additionnalEarth++;
                break;
            case Gameplay.Element.Water:
                additionnalWater++;
                break;
        }
    }

    void GenerateWave()
    {
        element = nextWaves.Dequeue();

        switch (Random.Range(0, 3)) {
            case 0:
                nextWaves.Enqueue(Gameplay.Element.Fire);
                break;
            case 1:
                nextWaves.Enqueue(Gameplay.Element.Earth);
                break;
            case 2:
                nextWaves.Enqueue(Gameplay.Element.Water);
                break;
        }

        UpdateTextWaves();

        for (int i = 0; i < (waveLevel / 2) + 4; i++) {
            Enemy enemy = Instantiate(Blob, new Vector3(-20, 7, 0) + Vector3.left * Random.Range(0, 2 + 0.5f * waveLevel), Quaternion.identity);
            enemy.SetPath(Path1);
            enemy.SetElement(element);
        }

        for (int i = 0; i < additionnalFire * ((waveLevel / 10) + 1); i++) {
            Enemy enemy = Instantiate(Blob, new Vector3(-20, 7, 0) + Vector3.left * Random.Range(0, 2 + 0.5f * waveLevel), Quaternion.identity);
            enemy.SetPath(Path1);
            enemy.SetElement(Gameplay.Element.Fire);
        }

        for (int i = 0; i < additionnalEarth * ((waveLevel / 10) + 1); i++) {
            Enemy enemy = Instantiate(Blob, new Vector3(-20, 7, 0) + Vector3.left * Random.Range(0, 2 + 0.5f * waveLevel), Quaternion.identity);
            enemy.SetPath(Path1);
            enemy.SetElement(Gameplay.Element.Earth);
        }

        for (int i = 0; i < additionnalWater * ((waveLevel / 10) + 1); i++) {
            Enemy enemy = Instantiate(Blob, new Vector3(-20, 7, 0) + Vector3.left * Random.Range(0, 2 + 0.5f * waveLevel), Quaternion.identity);
            enemy.SetPath(Path1);
            enemy.SetElement(Gameplay.Element.Water);
        }

        additionnalFire = 0;
        additionnalEarth = 0;
        additionnalWater = 0;

        waveLevel++;
    }

    private void GenerateEnemy(int pathNumber, Gameplay.Element element) {
        Vector3 initialPosition;
        Vector3 positionShift;
        List<Vector3> path;

        switch(pathNumber) {
            case 1:
                initialPosition = new Vector3(-20, 7, 0);
                positionShift = Vector3.left;
                path = Path1;
                break;
            case 2:
                initialPosition = new Vector3(20, 7, 0);
                positionShift = Vector3.right;
                path = Path2;
                break;
            default:
                Debug.LogError("Incorrect path number: " + pathNumber);
                return;
        }

        Enemy enemy = Instantiate(Blob, initialPosition + positionShift * Random.Range(0, 2 + 0.5f * waveLevel), Quaternion.identity);
        enemy.SetPath(path);
        enemy.SetElement(element);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
