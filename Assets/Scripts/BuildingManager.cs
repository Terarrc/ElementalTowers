﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public Building tower;
    bool previewMode = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (previewMode) {
            int mask = 1 << LayerMask.NameToLayer("Path");
            mask += 1 << LayerMask.NameToLayer("Buildings");

            Vector2 rectifiedPos = SnapToGrid();
            tower.transform.position = rectifiedPos;
            Collider2D towerCollider = Physics2D.OverlapCircle(rectifiedPos, 0.01f, mask); 

            SpriteRenderer towerSpriteRenderer = tower.GetComponent<SpriteRenderer>();
            if (towerCollider == null) {
                // Can build
                towerSpriteRenderer.color = new Color(0, 255, 0, 0.5f);
                if (Input.GetMouseButtonDown(0)) {
                    PlaceBuilding(rectifiedPos);
                } else if (Input.GetMouseButtonDown(1)) {
                    StopBuildingMode();
                }
            } else {
                // Not allowed to build
                towerSpriteRenderer.color = new Color(255, 0, 0, 0.5f);
            }
        }
    }

    public void StartBuildingMode() {
        previewMode = true;
        tower.gameObject.SetActive(true);
    }

    public void StopBuildingMode() {
        previewMode = false;
        tower.gameObject.SetActive(false);
    }

    private Vector2 SnapToGrid() {
        Vector3 m_MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_MousePosition.x = Mathf.Floor(m_MousePosition.x) + 0.5f;
        m_MousePosition.y = Mathf.Floor(m_MousePosition.y) + 0.3f;

        return (Vector2) m_MousePosition;
    }

    private void PlaceBuilding(Vector2 v2) {
        Debug.Log(this.GetType().Name + " - PlaceBuilding()");
        Building o = Instantiate(tower, v2, Quaternion.identity);

        SpriteRenderer towerSpriteRender = o.GetComponent<SpriteRenderer>();
        towerSpriteRender.color = Color.white;
    }
}
