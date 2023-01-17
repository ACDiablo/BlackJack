using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    TurnoManager turnManager;
    [Header("Game things")]
    [SerializeField] Deck deck;
    [SerializeField] List<Player> players = new List<Player>();
    [SerializeField] int rounds;
    [SerializeField] int currentRound,currentPlayer,currentCart;
    [SerializeField] int totalBet;
    [SerializeField] int initMoney;
    public enum GameState { CreatingParty, OnIAturn,OnPlayerTurn,waitingPlayerFinishTurn,WaitingNextPlayer}
    [SerializeField]GameState state;
    [Header("Panels")]
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject createGamePanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject waitingForNextPanel;
    [SerializeField] GameObject onTurnPanel;
    [SerializeField] GameObject waitingFinishTurnPanel;
    [Header("On player turn")]
    [SerializeField] TextMeshProUGUI playerMoney;
    private void Awake()
    {

        #region Singleton
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        #endregion
        turnManager = GetComponent<TurnoManager>();
        ShuffleDeck();
        
    }
    void Update()
    {
        /*if(Input.GetMouseButtonDown(0))
        {
            CreateGame(3, 2);
        }*/
    }
    #region StatesMachine
    public void OnDoAction()
    {
        switch (state)
        {

            case GameState.CreatingParty:
                break;
            case GameState.OnIAturn:
                break;
            case GameState.OnPlayerTurn:
                onTurnPanel.SetActive(false);
                ChangeState(GameState.waitingPlayerFinishTurn);
                break;
            case GameState.waitingPlayerFinishTurn:
                currentPlayer++;
                if (currentPlayer > players.Count - 1)
                {
                    currentPlayer = 0; 
                    currentRound++;
                    if(currentRound>rounds)
                    {
                        FinishGame();
                    }
                }
                ChangeState(GameState.WaitingNextPlayer);
                break;
            case GameState.WaitingNextPlayer:
                waitingForNextPanel.SetActive(false);
                gamePanel.SetActive(true);
                ChangeState(GameState.OnPlayerTurn);
                break;
            default:
                break;
        }
    }
    public void ChangeState(GameState _state)
    {
        switch (_state)
        {
            case GameState.CreatingParty:
                break;
            case GameState.OnIAturn:
                break;
            case GameState.OnPlayerTurn:
                UpdateUI();
                onTurnPanel.SetActive(true);
                turnManager.StartTurn(players[currentPlayer]);
                break;
            case GameState.waitingPlayerFinishTurn:
                waitingFinishTurnPanel.SetActive(true);
                break;
            case GameState.WaitingNextPlayer:
                string newText = "Turno del jugador " + (currentPlayer + 1 ).ToString()+ ".\nPulsa el botón cuando tengas el movil para continuar.";
                waitingForNextPanel.SetActive(true);
                waitingForNextPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = newText;
                break;
            default:
                break;
        }
        state = _state;
    }

    #endregion
    void UpdateUI()
    {
        playerMoney.text = "Dinero: "+ players[currentPlayer].money;
    }
    public void GetCart(bool _ask)
    {
        if(_ask)
        {
            Cart newCart = deck.runTimeDeck[currentCart];
            Player playerNow = players[currentPlayer];
            playerNow.carts.Add(newCart);
            newCart.AddValue(playerNow);
            currentCart++;
            /*switch (newCart.type)
            {
                case CartType.Normal:
                    newCart.AddValue(playerNow);
                    break;
                case CartType.As:
                    new
                    break;
                case CartType.Action:
                    break;
                default:
                    break;
            }*/

            //actualizar lo que haga que el player vea sus cartas
            //hacer lo que la carta haga
        }
        ChangeState(GameState.waitingPlayerFinishTurn);
    }
    public void CreateGame(int _players,int _rounds)
    {
        rounds = _rounds;
        for (int i = 0; i < _players; i++)
        {
            Player newPlayer = new Player(initMoney, i+1, false);
            players.Add(newPlayer);
        }
        currentPlayer = 0;currentRound = 0;
        ChangeState(GameState.WaitingNextPlayer);
    }
    void ShuffleDeck()
    {
        for (int i = deck.runTimeDeck.Count - 1; i >= 0; i--)
        {
            if(deck.runTimeDeck[i]!=null)
            deck.runTimeDeck.RemoveAt(i);
        }
        for (int i = 0; i < deck.deck.Length; i++)
        {
            deck.runTimeDeck.Add(deck.deck[i]);
        }
        for (int i = 0; i < deck.runTimeDeck.Count; i++)
        {
            Cart a = deck.runTimeDeck[i];
            int rand = Random.Range(i, deck.runTimeDeck.Count - 1);
            deck.runTimeDeck[i] = deck.runTimeDeck[rand];
            deck.runTimeDeck[rand] = a;
        }
        currentCart = 0;
    }
    void FinishGame()
    {

    }
}
