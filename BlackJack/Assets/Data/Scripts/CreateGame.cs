using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGame : MonoBehaviour
{
    int players;
    int rounds;

    void CreateAGame()
    {
        GameManager.instance.CreateGame(players, rounds);
    }

}
