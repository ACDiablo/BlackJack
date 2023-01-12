using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGame : MonoBehaviour
{
    int players;
    int rounds;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    void CreateAGame()
    {
        GameManager.instance.CreateGame(players, rounds);
    }

}
