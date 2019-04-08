using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceFighter : MonoBehaviour
{

    float minX = -2f;
    float maxX = 2f;
    float minY = -0.5f;
    float maxY = 1.5f;
    float padding = 0.1f;
    Vector3 worldMousePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = GetPos(worldMousePos);
        transform.position = playerPos;
    }

    private Vector2 GetPos(Vector2 worldPos)
    {
        float newX = Mathf.Clamp(worldPos.x, minX + padding, maxX - padding);
        float newY = Mathf.Clamp(worldPos.y, minY + padding, maxY - padding);
        Vector2 finalPos = new Vector2(newX, newY);
        return finalPos;
    }
}
