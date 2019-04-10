using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceFighter : MonoBehaviour
{

    float minX = -2f;
    float maxX = 2f;
    float minY = -0.5f;
    float maxY = 1.5f;
    float padding = 0.2f;
    float moveSpeed = 3f;
    Vector3 worldMousePos;
    // Start is called before the first frame update
    void Update()
    {
        Move();
    }

    private void Move(){
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime*moveSpeed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime*moveSpeed;           
        float newX = Mathf.Clamp(transform.position.x + deltaX, minX + padding, maxX - padding);
        float newY = Mathf.Clamp(transform.position.y + deltaY, minY + padding, maxY - padding);
        Vector2 playerPos = new Vector2(newX,newY);
        // Debug.Log("playerPos:"+ playerPos);
        transform.position = playerPos;
    }

}
