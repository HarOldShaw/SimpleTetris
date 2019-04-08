using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitController : MonoBehaviour
{
    int[,] particleMatrix;
    int rowLength;
    int colLength;

    void Start()
    {
        particleMatrix = new int[5, 5];
        rowLength = particleMatrix.GetLength(0);
        colLength = particleMatrix.GetLength(1);
    }

    public void AddParticle(int x, int y, int value)
    {
        particleMatrix[x, y] = value;
       // CheckBrokenCondition();

    }

    /*
     private void CheckBrokenCondition()
    {

    }
    */

    public void PrintMatrixContent()
    {
        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                Console.Write(string.Format("{0} ", particleMatrix[i, j]));
            }
            Console.Write(Environment.NewLine + Environment.NewLine);
        }
        Console.ReadLine();
    }


    public Orbit GetOrbitByIndex(int i)
    {
        return transform.GetChild(i).GetComponent<Orbit>();
    }
}
