using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnoManager : MonoBehaviour
{
    Player currentPlayer;
    public void SetPlayer(Player _player) { currentPlayer = _player; }
    public enum TurnState { Betting, Askingcart}
    public TurnState state;
    bool inTurn;
    int betAmount;
    [Header("Panels")]
    [SerializeField] GameObject betPanel;
    [SerializeField] GameObject askingPanel;

    public void OnDoAction(bool _ask )
    {
        StartCoroutine(AskingCart(_ask));
    }
    public void Double()//TO DO:: hacer todo lo de doblar
    {
        StartCoroutine(Doblar());
    }
    public void OnDoAction(int _amount)
    {
        StartCoroutine(Betting(_amount));
    }
    void ChangeState(TurnState _state)
    {
        switch (_state)
        {
            case TurnState.Betting:
                askingPanel.SetActive(false);
                betPanel.SetActive(true);
                betAmount = 0;
                break;
            case TurnState.Askingcart:
                betPanel.SetActive(false);
                askingPanel.SetActive(true);
                break;
            default:
                break;
        }
        state = _state;
    }
    public void StartTurn(Player _player)
    {
        currentPlayer = _player;
        betAmount = 0;
        GameManager.instance.PrintCards(currentPlayer);
        ChangeState(TurnState.Betting);
    }


    #region Corrutinas
    IEnumerator AskingCart(bool _ask)
    {
        if(_ask)
        {
            if(currentPlayer.carts.Count <=0)
                GameManager.instance.GetCart(_ask);
            GameManager.instance.GetCart(_ask);
            if(currentPlayer.score>21&&currentPlayer.score2>21)
            {
                FinishTurn();
            }
        }
        else
        {
            FinishTurn();
        }
        yield break;
    }
    void FinishTurn()
    {
        GameManager.instance.FinishTurn();
        askingPanel.SetActive(false);
    }
    IEnumerator Betting(int _amount)
    {
        betAmount = _amount;
        currentPlayer.money -= _amount;
        GameManager.instance.UpdateUI(currentPlayer);
        GameManager.instance.UpdateBet(betAmount);
        ChangeState(TurnState.Askingcart);
        yield break;
    }
    IEnumerator Doblar()
    {
        currentPlayer.money -= betAmount;
        betAmount *= 2;
        GameManager.instance.GetCart(true);
        FinishTurn();
        yield break;
    }
    #endregion

}
