using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] Transform gridCards;
    [SerializeField] GameObject cardPrefab;
    [Header("ThingsToWin")]
    Player roundWinner;
    int roundMaxScore;
    int totalWinnerFinishMoney;
    Player finalWinnerPlayer;
    [SerializeField] GameObject showRoundWinnerPanel;
    [SerializeField] GameObject showFinalWinnerPanel;

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
    [SerializeField] TextMeshProUGUI playerIdText;
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
#if UNITY_EDITOR
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
        }
    }
#endif
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
                waitingFinishTurnPanel.SetActive(false);
                if (currentPlayer > players.Count - 1)
                {
                    currentPlayer = 0; 
                    currentRound++;
                    WinRound();
                    if(currentRound>rounds)
                    {
                        FinishGame();
                    }
                    return;
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
                UpdateUI(players[currentPlayer]);
                onTurnPanel.SetActive(true);
                gridCards.gameObject.SetActive(true);
                turnManager.StartTurn(players[currentPlayer]);
                break;
            case GameState.waitingPlayerFinishTurn:
                waitingFinishTurnPanel.SetActive(true);
                break;
            case GameState.WaitingNextPlayer:
                string newText = "Turno del jugador " + (currentPlayer + 1 ).ToString()+ ".\nPulsa el botón cuando tengas el movil para continuar.";
                waitingForNextPanel.SetActive(true);
                waitingForNextPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = newText;
                gridCards.gameObject.SetActive(false);
                break;
            default:
                break;
        }
        state = _state;
    }

    #endregion
    #region UI
    public void UpdateUI(Player _currentPlayer)
    {
        playerMoney.text = "Dinero: "+ players[currentPlayer].money;
        playerIdText.text = "Player " + players[currentPlayer].idPlayer;
        PrintCards(_currentPlayer);
    }
    public void PrintCards(Player _currentPlayer)
    {
        for (int i = gridCards.childCount - 1; i >= 0; i--)
        {
            Destroy(gridCards.GetChild(i).gameObject);
        }
        for (int i = 0; i < _currentPlayer.carts.Count; i++)
        {
            GameObject newCart = Instantiate(cardPrefab, gridCards);
            newCart.GetComponent<Image>().sprite = _currentPlayer.carts[i].cardSprite;
        }
    }
    #endregion
    #region runGame functions
    public void GetCart(bool _ask)
    {
        if(_ask)
        {
            Cart newCart = deck.runTimeDeck[currentCart];
            Player playerNow = players[currentPlayer];
            playerNow.carts.Add(newCart);
            currentCart++;

            switch (newCart.type)
            {
                case CartType.Normal:
                    newCart.AddValue(playerNow);
                    break;
                case CartType.As:
                    newCart.AddValue(playerNow);
                    break;
                case CartType.Action:
                    
                    break;
                default:
                    break;
            }

            //hacer lo que la carta haga
        }
        UpdateUI(players[currentPlayer]);
    }
    public void FinishTurn()
    {
        ChangeState(GameState.waitingPlayerFinishTurn);
    }
    public void ChangeCartsAction()
    {
        List<Cart> currentList = players[currentPlayer].carts;
        int randomPlayer = Random.Range(0, currentPlayer);
        players[currentPlayer].carts = players[randomPlayer].carts;
        players[randomPlayer].carts = currentList;
        UpdateUI(players[currentPlayer]);
    }
    #endregion
    #region UpdateBet
    public void UpdateBet(int _bet)
    {
        totalBet += _bet;
    }
    void SetBet(int _bet)
    {
        totalBet = _bet;
    }
    #endregion
    #region StartGame
    public void CreateGame(int _players,int _rounds)
    {
        rounds = _rounds;
        for (int i = 0; i < _players-1; i++)
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
    #endregion
    void WinRound()
    {
        for (int i = 0; i < players.Count; i++)
        {
            int tempScore = players[i].score;
            if (tempScore < players[i].score2&&players[i].score2<=21)
                tempScore = players[i].score2;
            if(tempScore<=21&&tempScore>=roundMaxScore)
            {
                roundMaxScore = tempScore;
                roundWinner = players[i];
            }
        }
        for (int i = 0; i < players.Count; i++)
        {
            players[i].score = 0;
            players[i].score2 = 0;
            for (int j = 0; j < players[i].carts.Count; j++)
            {
                players[i].carts.RemoveAt(j);
            }
        }
        if (roundWinner == null)
            return;
        roundWinner.money += totalBet;
        string winnerString = "Ha ganado el jugador "+roundWinner.idPlayer+" con "+roundMaxScore+" puntos. \n +"+totalBet+" euros.";
        StartCoroutine(ShowroundWinner(winnerString));
    }
    IEnumerator ShowroundWinner(string _wintext)
    {
        showRoundWinnerPanel.SetActive(true);
        TextMeshProUGUI text = showRoundWinnerPanel.GetComponent<WinnerUiPanel>().GetText();
        text.text = _wintext;
        yield break;
    }
    void FinishGame()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if(players[i].money>totalWinnerFinishMoney)
            {
                totalWinnerFinishMoney = players[i].money;
                finalWinnerPlayer = players[i];
            }
        }
        showFinalWinnerPanel.SetActive(true);
        TextMeshProUGUI text = showFinalWinnerPanel.GetComponent<WinnerUiPanel>().GetText();
        string newString = "Ha ganado el jugador " + roundWinner.idPlayer + " con un total de "+finalWinnerPlayer.money +" euros.";
        text.text = newString;
    }
}
