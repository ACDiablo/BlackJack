using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    enum GameState { CreatingParty, OnIAturn,OnPlayerTurn,WaitingNextPlayer}
    GameState state;
    [Header("Game things")]
    List<Cart> deck = new List<Cart>();
    List<Player> players = new List<Player>();
    [SerializeField] int rounds;
    [SerializeField] int currentRounds;
    private void Awake()
    {

        #region Singleton
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        #endregion
        ShuffleDeck();

    }
    void Update()
    {
        switch (state)
        {
            case GameState.CreatingParty:
                break;
            case GameState.OnIAturn:
                break;
            case GameState.OnPlayerTurn:
                break;
            case GameState.WaitingNextPlayer:
                break;
            default:
                break;
        }
    }
    
    public void CreateGame(int _players,int _rounds)
    {

    }
    void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Cart a = deck[i];
            int rand = Random.Range(i, deck.Count - 1);
            deck[i] = deck[rand];
            deck[rand] = a;
        }
    }
}
