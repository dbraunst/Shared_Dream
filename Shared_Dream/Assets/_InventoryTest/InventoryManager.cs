using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    /*
        Inventory Manager oversees all Player Inventories in the game. 
        It referenced and mostly called from ObjectSpawner
    */

    public GameObject[] collectableObjects;

    // TODO: Split inventory display into Inventory and Display
    public PlayerInventoryDisplay[] playerInventories;

    private int playerIdx = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerInventories = GameObject.FindObjectsOfType<PlayerInventoryDisplay>();
    }

    //Called from Object Spawner First
    public void AddSpawnedObjectArray(GameObject[] _spawnedObj)
    {
        collectableObjects = _spawnedObj;
    }

    // Called from Object Spawner Second. "Randomly" assigns objects to players by 
    // iterating through until it's complete
    public void AssignObjectsToPlayers()
    {
        for(int i = 0; i < collectableObjects.Length; i++){
            playerInventories[playerIdx++].objectsToCollect.Add(collectableObjects[i]);
            playerIdx %= playerInventories.Length;
        }
    } 

    // Called form Object Spawner Third
    public void InitializePlayerInventories(){
        foreach(PlayerInventoryDisplay _player in playerInventories)
        {
            _player.IntializeInventoryDisplay();
        }
    }
}
