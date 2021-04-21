using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public GameObject[] collectableObjects;

    public PlayerInventory[] players;

    private int playerIdx = 0;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindObjectsOfType<PlayerInventory>();
        AssignObjectsToPlayers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddSpawnedObjectList(GameObject[] _spawnedObj)
    {
        collectableObjects = _spawnedObj;
    }

    public void AssignObjectsToPlayers()
    {
        for(int i = 0; i < collectableObjects.Length; i++){
            players[playerIdx++].objectsToCollect.Add(collectableObjects[i]);
            playerIdx %= players.Length;
        }
    } 
}
