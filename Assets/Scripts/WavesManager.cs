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
    public Enemy Boss;

    public int waveLevel;
    public int additionnalFire;
    public int additionnalEarth;
    public int additionnalWater;

    public Text wavePreview;

    struct Wave
    {
        public Wave(Gameplay.Element e, int lvl)
        {
            elem = e;
            level = lvl;
        }
        public Gameplay.Element elem;
        public int level;
    }

    private Queue<Wave> nextWaves = new Queue<Wave>();

    // Start is called before the first frame update
    void Start()
    {
        gameplay.timerWaveEvent += GenerateWave;
        gameplay.swapEvent += SwapElement;

        switch (gameplay.PlayerElement) {
            case Gameplay.Element.Fire:
                nextWaves.Enqueue(new Wave(Gameplay.Element.Earth, waveLevel));
                break;
            case Gameplay.Element.Earth:
                nextWaves.Enqueue(new Wave(Gameplay.Element.Water, waveLevel));
                break;
            case Gameplay.Element.Water:
                nextWaves.Enqueue(new Wave(Gameplay.Element.Fire, waveLevel));
                break;
        }
        waveLevel++;

        for (int i = 0; i < 7; i++) {
            switch (Random.Range(0, 3)) {
                case 0:
                    nextWaves.Enqueue(new Wave(Gameplay.Element.Fire, waveLevel));
                    break;
                case 1:
                    nextWaves.Enqueue(new Wave(Gameplay.Element.Earth, waveLevel));
                    break;
                case 2:
                    nextWaves.Enqueue(new Wave(Gameplay.Element.Water, waveLevel));
                    break;
            }
            waveLevel++;
        }

        UpdateTextWaves();
    }

    void UpdateTextWaves()
    {
        string message = "";
        int i = 0;
        foreach (Wave wave in nextWaves) {
            if ((wave.level + 1) % 10 == 0) {
                message += "<color=purple>X</color>";
            } else {
                switch (wave.elem) {
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
        Wave newWave = nextWaves.Dequeue();

        switch (Random.Range(0, 3)) {
            case 0:
                nextWaves.Enqueue(new Wave(Gameplay.Element.Fire, waveLevel));
                break;
            case 1:
                nextWaves.Enqueue(new Wave(Gameplay.Element.Earth, waveLevel));
                break;
            case 2:
                nextWaves.Enqueue(new Wave(Gameplay.Element.Water, waveLevel));
                break;
        }
        waveLevel++;

        UpdateTextWaves();

        if ((newWave.level + 1) % 10 == 0) {
            for (int i = 0; i < (newWave.level + 1) / 10; i++) {
                GenerateBoss(1, newWave.level);
            }   
        } else {
            for (int i = 0; i < (newWave.level / 2) + 4; i++) {
                GenerateBlob(1, newWave.elem, newWave.level);
            }
        }

        for (int i = 0; i < additionnalFire * ((newWave.level / 10) + 1); i++) {
            GenerateBlob(1, Gameplay.Element.Fire, newWave.level);
        }

        for (int i = 0; i < additionnalEarth * ((newWave.level / 10) + 1); i++) {
            GenerateBlob(1, Gameplay.Element.Earth, newWave.level);
        }

        for (int i = 0; i < additionnalWater * ((newWave.level / 10) + 1); i++) {
            GenerateBlob(1, Gameplay.Element.Water, newWave.level);
        }

        additionnalFire = 0;
        additionnalEarth = 0;
        additionnalWater = 0;
    }

    private void GenerateBlob(int pathNumber, Gameplay.Element element, int level) {
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

        Enemy enemy = Instantiate(Blob, initialPosition + positionShift * Random.Range(0, 2 + 0.5f * level), Quaternion.identity);
        enemy.SetPath(path);
        enemy.SetElement(element);
        enemy.GetComponent<Health>().killedEvent += gameplay.IncreaseResource;
    }

    private void GenerateBoss(int pathNumber, int level)
    {
        Vector3 initialPosition;
        Vector3 positionShift;
        List<Vector3> path;

        switch (pathNumber) {
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

        Enemy enemy = Instantiate(Boss, initialPosition + positionShift * Random.Range(0, ((level + 1) / 10)), Quaternion.identity);
        enemy.SetPath(path);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
