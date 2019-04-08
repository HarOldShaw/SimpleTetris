using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] Particle[] particlePrefabArray;
    [SerializeField] float minSpawnDelay = 1f;
    [SerializeField] float maxSPawnDelay = 3f;
    [SerializeField] Transform startingPoint;
    bool isSpawning = true;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSPawnDelay));
            SpawnParticles();
        }
    }

    private void SpawnParticles()
    {
        var particleIndex = Random.Range(0, particlePrefabArray.Length);
        Particle myParticle = particlePrefabArray[particleIndex];
        Particle newParticle = Instantiate(myParticle, startingPoint.position, transform.rotation) as Particle;
        newParticle.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
