using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;

    [Header("Components need to be assigned")]
    [SerializeField] OrbitController orbitController;
    [SerializeField] int colorValue = 0;

    int currentOrbitIndex;
    int currentChildIndex;
    bool findTarget = false;
    Vector3 targetPos;
    float gap = 1f;
    bool hasCollided = false;

    
    static string FIRST_ORBIT_NAME = "Orbit 1";
    static string SECOND_ORBIT_NAME = "Orbit 2";
    static string THIRD_ORBIT_NAME = "Orbit 3";
    static string FOURTH_ORBIT_NAME = "Orbit 4";
    static string FIFTH_ORBIT_NAME = "Orbit 5";

    // Start is called before the first frame update
    void Start()
    {
       //Debug.Log("this color:" + colorValue);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
        if (findTarget)
        {
            var movementThisFrame = moveSpeed * Time.deltaTime;
            Vector3 newPos = new Vector3(targetPos.x, transform.position.y, transform.position.z);
            transform.position = Vector2.MoveTowards(
                transform.position, newPos, movementThisFrame
                );
        }
        if (transform.position == targetPos)
        {
            findTarget = false;
        }
    }


    public void setMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<OrbitSpawner>())
        {
            Debug.Log("On trigger enter");
            targetPos = FindTarget().position;
            findTarget = true;
          }
        else if (other.gameObject.GetComponent<Particle>() || other.gameObject.GetComponent<BottomCollider>())
        {
            if (!hasCollided) { 
                setMoveSpeed(0);
                transform.position = GetNearestPoint();
                SetOrbitParent();
                GetComponent<CircleCollider2D>().isTrigger = true;
                // TODO add the value to particle matrix
                CheckBrokenCondition();

                // avoid triggered twice
                hasCollided = true;
            }
        }
    }

    // check if any broken conditions are met
    private void CheckBrokenCondition()
    {
        // List<GameObject> destroyList = new List<GameObject>();
        // Orbit thisOrbit = orbitController.getOrbitByIndex(currentOrbitIndex);
        // check if there is particile in the same orbit with the same color

        //check is neighbor orbit has a particle in the same row with the same color
         
        return;
    }


    // align particle to nearest orbit based on the x position
    private void SetOrbitParent()
    {
        int newInt = (int)transform.position.x;
        int orbitIndex = -1;
        Transform parentTransform;
        if (newInt == -2)
        {
            parentTransform = GameObject.Find(FIRST_ORBIT_NAME).transform;
            orbitIndex = 0;
        } else if (newInt == -1)
        {
            parentTransform = GameObject.Find(SECOND_ORBIT_NAME).transform;
            orbitIndex = 1;
        } else if(newInt == 0)
        {
            parentTransform = GameObject.Find(THIRD_ORBIT_NAME).transform;
            orbitIndex = 2;
        } else if (newInt == 1)
        {
            parentTransform = GameObject.Find(FOURTH_ORBIT_NAME).transform;
            orbitIndex = 3;
        } else if (newInt == 2)
        {
            parentTransform = GameObject.Find(FIFTH_ORBIT_NAME).transform;
            orbitIndex = 4;
        }
        else
        {
            Debug.Log("wrong place");
            return;
        }
        // set the parent of the particle
        gameObject.transform.SetParent(parentTransform,true);
        currentOrbitIndex = orbitIndex;
        Debug.Log("Current Orbit: " + currentOrbitIndex);
        Orbit thisOrbit = parentTransform.gameObject.GetComponent<Orbit>();
        if (thisOrbit)
        {
            currentChildIndex = thisOrbit.GetCurrentIndex();
            thisOrbit.addChildIndex();

            Debug.Log("X: " + currentOrbitIndex + " Y: " + currentChildIndex + " value: " + colorValue);
            orbitController.AddParticle(currentOrbitIndex, currentChildIndex, colorValue);
            orbitController.PrintMatrixContent();
            //Debug.Log("current index: "+thisOrbit.GetCurrentIndex());
        }
    }

    //find the nearest orbit fix point
    private Vector3 GetNearestPoint()
    {
        var newX = Mathf.Round(transform.position.x * 2) / 2;
        var newY = Mathf.Round(transform.position.y * 2) / 2;
        return new Vector3(newX, newY, 0);
    }
    
    // find the transform of nearest orbit spawner;
    private Transform FindTarget()
    {
        OrbitSpawner[] orbitSpawners = FindObjectsOfType<OrbitSpawner>();
        float minDistance = Mathf.Infinity;
        Transform closestOrbitPoint;
        if (orbitSpawners.Length == 0) { return null; }
        closestOrbitPoint = orbitSpawners[0].gameObject.transform;
        for(int i = 0; i<orbitSpawners.Length; i++)
        {
            float distance = (orbitSpawners[i].transform.position - transform.position).sqrMagnitude;
            if(distance < minDistance)
            {
                closestOrbitPoint = orbitSpawners[i].gameObject.transform;
                minDistance = distance;
            }
        }
        return closestOrbitPoint;
    }

    //get the color of the particle
    public Color getColor()
    {
        return GetComponent<SpriteRenderer>().color;
    }

    //test
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<OrbitSpawner>())
        {   
            //GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Discrete
        }       
    }
}
