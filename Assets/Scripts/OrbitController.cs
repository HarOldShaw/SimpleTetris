using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitController : MonoBehaviour
{
    int[,] particleMatrix = new int[5,5];
    int[,] destroyMatrix = new int[5,5];
    int rowLength;
    int colLength;


    void Awake()
    {
        rowLength = particleMatrix.GetLength(0);
        colLength = particleMatrix.GetLength(1);
        PrintMatrixContent();
    }

    public void AddParticle(int x, int y, int value)
    {
        // Debug.Log("adding particle: "+x+" "+y+" "+value);
        particleMatrix[x, y] = value;
        CheckBrokenCondition(x,y);

    }
    
    public void CheckBrokenCondition(int x, int y)
    {        
        PrintMatrixContent();
        //compare down
        if(y>=2){
            if(CheckDown(x,y)){
                if(CheckDown(x,y-1)){
                    MarkAsDestory(x,y);
                    MarkAsDestory(x,y-1);
                    MarkAsDestory(x,y-2);
                }
            }
        }
        //look left
        if(x>=2){
            if(CheckLeft(x,y)){
                if(CheckLeft(x-1,y)){
                    MarkAsDestory(x,y);
                    MarkAsDestory(x-1,y);
                    MarkAsDestory(x-2,y);
                }
            }
        }

        //look right
        if(x<=2){
            if(CheckRight(x,y)){
                if(CheckRight(x+1,y)){
                    MarkAsDestory(x,y);
                    MarkAsDestory(x+1,y);
                    MarkAsDestory(x+2,y);
                }
            }
        }

        //look both side
        if((x>=1 && x<=3)){
            if(CheckLeft(x,y) && CheckRight(x,y)){
                MarkAsDestory(x,y);
                MarkAsDestory(x-1,y);
                MarkAsDestory(x+1,y);
            }
        }

        //Handle Particle Destroy
        HandleParticleDestroy(x,y);
    }

    private void HandleParticleDestroy(int x, int y){
        // from top to bottom
         for(int i = rowLength-1; i>=0; i--){
            for(int j = colLength-1; j>=0;j--){
                if(destroyMatrix[i,j] == 1){ 
                    DestroyParticle(i,j);
                }
            }
        }
        //reset the destroy matrix
        ResetDestroyMatrix();
    }   

    private void DestroyParticle(int x, int y){
        Orbit thisOrbit = GetOrbitByIndex(x);
        Particle target = thisOrbit.GetParticle(y);
    
        // Debug.Log("Current Orbit: "+x+ "index: "+GetOrbitByIndex(x).GetCurrentIndex()+ "child count:" + thisOrbit.transform.childCount);
        StartCoroutine(ToDestroy(target,x,y));
        //let the particle fall down,
     
        
    }

    IEnumerator ToDestroy(Particle obj, int x, int y){
        obj.Explode();
        GetOrbitByIndex(x).ReduceChildtIndex();
        // adjust the particle matrix, replace yth element by y+1 th element
        for(int i = y; i<colLength-1; i++){
            particleMatrix[x,i] = particleMatrix[x,i+1];
            particleMatrix[x,colLength-1] = 0;
        }
        PrintMatrixContent();
        yield return new WaitForSeconds(0.1f);
        Orbit newOrbit = GetOrbitByIndex(x);
        // Debug.Log("After destroy Orbit "+x+ "index: "+newOrbit.GetCurrentIndex()+ "child count:" + newOrbit.transform.childCount);     
        if(particleMatrix[x,y] != 0){
        for(int i=y; i<newOrbit.GetCurrentIndex(); i++){
                // Debug.Log("i: "+i);
                // Debug.Log("orbit current index: "+newOrbit.GetCurrentIndex()+"particle color:"+newOrbit.GetParticle(i).getColor());
                newOrbit.GetParticle(i).ResetParticle(x,i);
            }
        }
    }

    public void MarkAsDestory(int x, int y){
        destroyMatrix[x,y] = 1;
    }

    public void ResetDestroyMatrix(){
        for(int i = 0; i<rowLength; i++){
            for(int j = 0; j<colLength;j++){
                destroyMatrix[i,j] = 0;
            }
        }
    }

   
    public Orbit GetOrbitByIndex(int i)
    {
        return transform.GetChild(i).gameObject.GetComponent<Orbit>();
    }

    
    public bool CheckDown(int x, int y){
        if (particleMatrix[x,y] == particleMatrix[x,y-1]){
            //Debug.Log("checkdown: true");
            return true;
        }else
        {
            return false;
        }
    }

    public bool CheckLeft(int x, int y){
        if (particleMatrix[x,y] == particleMatrix[x-1,y]){
            // Debug.Log("checkleft: true");
            return true;
        }else
        {
            return false;
        }
    }

    public bool CheckRight(int x, int y){
        if (particleMatrix[x,y] == particleMatrix[x+1,y]){
            // Debug.Log("checkright: true");
            return true;
        }else
        {
            return false;
        }
    }
 
    //TEST 
    public void PrintMatrixContent()
    {
        string arrayString = "";
        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                arrayString += string.Format("{0} ", particleMatrix[i, j]);
            }
            arrayString += (System.Environment.NewLine + System.Environment.NewLine);
        }
        Debug.Log("arrayString: "+arrayString);
    }



}
