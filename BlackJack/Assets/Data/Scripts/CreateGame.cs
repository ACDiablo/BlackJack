using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGame : MonoBehaviour
{
    int players;
    int rounds;
    [SerializeField] GameObject playersPanel, roundsPanel;
    private void OnEnable()
    {
        playersPanel.SetActive(true);
        roundsPanel.SetActive(false);
        players = rounds = 0;
    }
    public void SetRoundsAndCreateAGame(int _rounds)
    {
        rounds = _rounds;
        GameManager.instance.CreateGame(players, rounds);
        gameObject.SetActive(false);
    }
    public void SetPlayers(int _players)
    {
        players = _players;
        playersPanel.SetActive(false);
        roundsPanel.SetActive(true);
    }

}
