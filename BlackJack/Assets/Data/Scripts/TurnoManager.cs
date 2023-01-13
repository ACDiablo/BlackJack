using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnoManager : MonoBehaviour
{
    Player currentPlayer;
    public void SetPlayer(Player _player) { currentPlayer = _player; }
    public enum TurnState { Betting, Askingcart}
    TurnState state;
    bool inTurn;
    int betAmount;
    [Header("Panels")]
    [SerializeField] GameObject betPanel;
    [SerializeField] GameObject askingPanel;
    public void OnDoAction(bool _ask = false )
    {
        StartCoroutine(AskingCart(_ask));
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
    }
    public void StartTurn(Player _player)
    {
        currentPlayer = _player;
        ChangeState(TurnState.Betting);
    }

    #region Corrutinas
    IEnumerator AskingCart(bool _ask)
    {
        GameManager.instance.GetCart(_ask);
        askingPanel.SetActive(false);
        yield break;
    }
    IEnumerator Betting(int _amount)
    {
        betAmount = _amount;
        currentPlayer.money -= _amount;
        ChangeState(TurnState.Askingcart);
        yield break;
    }
    #endregion

}
