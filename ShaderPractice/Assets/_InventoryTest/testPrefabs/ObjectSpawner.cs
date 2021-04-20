using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // Prefabs for the spawner to select from
    public GameObject[] spawnableObjectPrefabs;

    // Temporary reference to prefab copies, set in SpawnObjects
    // 
    public GameObject[] spawnedObjects; 

    // Array of 
    private GameObject[] spawnPoints;

    private int numObjectsToSpawn;

    private GameObject[] prefabsToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        // Queries the scene to grab all objects tagged "ObjectSpawn"
        spawnPoints = GameObject.FindGameObjectsWithTag("ObjectSpawn");
        numObjectsToSpawn = spawnPoints.Length;
    }
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)){
            Debug.Log("Num SpawnPoints: " + numObjectsToSpawn + "\nMax possible Obj: " + prefabsToSpawn.Length);
        }

        if (Input.GetKeyDown(KeyCode.S)){
            SpawnObjects();
        }
    }

    // Randomly Spawns Objects from the "Spawnable Object Prefabs" array
    // based on the number of spawn points in the scene. 
    // Requires more spawn points than spawnable objects.
    void SpawnObjects() {
        // Clear Existing Spawned Objects
        foreach (GameObject _spawnedObject in spawnedObjects) {
            Destroy(_spawnedObject);
        }

        RandomizeSpawnPointOrder();
        prefabsToSpawn = ChoosePrefabsToSpawn();
        
        // Set and fill local array of prefab clones 
        spawnedObjects = new GameObject[numObjectsToSpawn];
        for (int i = 0; i < spawnPoints.Length; i++){
            spawnedObjects[i] = GameObject.Instantiate(prefabsToSpawn[i], spawnPoints[i].transform.position,
                Quaternion.identity);
        }
    }

    // Shuffles the order of Spawn Points around to randomize spawn location
    void RandomizeSpawnPointOrder(){
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject temp = spawnPoints[i];
            int randomIdx = Random.Range(0, spawnPoints.Length);
            spawnPoints[i] = spawnPoints[randomIdx];
            spawnPoints[randomIdx] = temp;
        }
    }

    // Selects a random number of spawnable objects based on the number of objects in the scene
    // with the "SpawnPoint" tag. Returns an array of the selected GameObjects
    GameObject[] ChoosePrefabsToSpawn(){
        GameObject[] result = new GameObject[numObjectsToSpawn];

        int _numObjectsToSpawn = numObjectsToSpawn;

        // probability-based random selection from array. 
        for (int numLeft = spawnableObjectPrefabs.Length; numLeft > 0; numLeft--)
        {
            float prob = (float)_numObjectsToSpawn/(float)numLeft;

            if (Random.value <= prob) {
                _numObjectsToSpawn--;
                result[_numObjectsToSpawn] = spawnableObjectPrefabs[numLeft - 1];

                if (_numObjectsToSpawn == 0) {
                    break;
                }
            }
        }
        return result;
    }

    
}
