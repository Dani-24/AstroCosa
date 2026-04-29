
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] int stage = 0;
    [SerializeField] int score = 0;
    [SerializeField] float difficulty = 10;
    [SerializeField] List<GameObject> enemySquads = new();

    void Start()
    {
        
    }

    void Update()
    {
        if(enemySquads.Count <= 0)
        {
            // TODO: If there is no enemy squad,
            // spawns a random enemy squad from the available ones in this stage
        }
    }
}
