﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
	public Canvas canvas;
	public Gameplay gameplay;

    public Building tower;
    public Building resource;
    public Building buffer;
    public GameObject towerPreview;
    bool previewMode = false;
    public GameObject areaOfEffect;

    private Building chooseBuilding;

    // Start is called before the first frame update
    void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
        if (previewMode) {
			if (Input.GetMouseButtonDown(1)) {
				StopBuildingMode();
			}

			int mask = 1 << LayerMask.NameToLayer("Path");
            mask += 1 << LayerMask.NameToLayer("Buildings");

            Vector2 rectifiedPos = SnapToGrid();
            towerPreview.transform.position = rectifiedPos;
            Collider2D towerCollider = Physics2D.OverlapCircle(rectifiedPos, 0.01f, mask); 

            Vector2 centeredPos = rectifiedPos;
            centeredPos.x = Mathf.Floor(centeredPos.x) + 0.5f;
            centeredPos.y = Mathf.Floor(centeredPos.y) + 0.5f;
            areaOfEffect.transform.position = centeredPos;

            SpriteRenderer towerSpriteRenderer = towerPreview.GetComponent<SpriteRenderer>();
            if (towerCollider == null && chooseBuilding.cost <= gameplay.Gems) {
                // Can build
                towerSpriteRenderer.color = new Color(0, 255, 0, 0.5f);
                if (Input.GetMouseButtonDown(0)) {
                    PlaceBuilding(rectifiedPos);
                }
            } else {
                // Not allowed to build
                towerSpriteRenderer.color = new Color(255, 0, 0, 0.5f);
            }
		}
    }

    public void StartBuildingMode(int choice) {
        chooseBuilding =
            choice == 0 ? tower :
            choice == 1 ? resource :
            choice == 2 ? buffer :
            null;

        if (chooseBuilding == null) {
            Debug.Log("Choosed Building index does not exist : " + choice);
            return;
        }
        previewMode = true;
        towerPreview.gameObject.SetActive(true);
        towerPreview.gameObject.GetComponent<SpriteRenderer>().sprite = chooseBuilding.GetComponent<SpriteRenderer>().sprite;

        foreach (Transform child in canvas.transform) {
            if (!child.CompareTag("UiText")) {
                child.gameObject.SetActive(false);
            }
        }

        areaOfEffect.SetActive(true);
        Vector3 aoeScaleVector = areaOfEffect.transform.localScale;
        Weapon buildingWeapon = chooseBuilding.GetComponent<Weapon>();
        float range = (buildingWeapon != null ? buildingWeapon.range : 0) * 2 + 1;
        aoeScaleVector.x = range;
        aoeScaleVector.y = range;
        areaOfEffect.transform.localScale = aoeScaleVector;
    }

    public void StopBuildingMode() {
        previewMode = false;
        towerPreview.gameObject.SetActive(false);

        foreach (Transform child in canvas.transform) {
            child.gameObject.SetActive(true);
        }

        areaOfEffect.SetActive(false);
	}

    private Vector2 SnapToGrid() {
        Vector3 m_MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_MousePosition.x = Mathf.Floor(m_MousePosition.x) + 0.5f;
        m_MousePosition.y = Mathf.Floor(m_MousePosition.y) + 0.3f;

        return (Vector2) m_MousePosition;
    }

    private void PlaceBuilding(Vector2 v2) {
        if (chooseBuilding.cost <= gameplay.Gems) {
            gameplay.Gems -= chooseBuilding.cost;

            Building building = Instantiate(chooseBuilding, v2, Quaternion.identity);

            EntityElement towerElement = building.GetComponent<EntityElement>();
            towerElement.EnableSwapElement(gameplay);
            towerElement.Element = gameplay.PlayerElement;

            SpriteRenderer towerSpriteRender = building.GetComponent<SpriteRenderer>();
            towerSpriteRender.color = Color.white;


            ResourceGenerator rg = building.GetComponent<ResourceGenerator>();
            if (rg != null) {
                rg.gameplay = gameplay;
                towerElement.IsActive = true;
            }
        }
    }
}
