﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	// Start is called before the first frame update
	void Start()
    {
		gameplay.timerWaveEvent += GenerateWave;
		gameplay.swapEvent += SwapElement;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
