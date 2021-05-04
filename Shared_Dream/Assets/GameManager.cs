using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public static int MAX_OBJECTS_PER_PLAYER = 8;
    public static int NUM_PLAYERS = 1;

    public static int NUM_OBJECTS_PER_PLAYER = 3;

    //TODO: Needs Ctor from the intro scene / potential 'lobby'

    private void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
        }

        
    }
}
