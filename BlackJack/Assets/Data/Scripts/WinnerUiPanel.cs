using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinnerUiPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI winnerText;
    [SerializeField] float openSpeed;
    public TextMeshProUGUI GetText() { return winnerText; }
    
    void OnEnable()
    {
        transform.localScale = Vector3.zero;
    }

    void Update()
    {
        if(transform.localScale.x<0.995)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, openSpeed * Time.deltaTime);
        }
    }
    public void ClosePanel()
    {
        gameObject.SetActive(false);
        GameManager.instance.ChangeState(GameManager.GameState.WaitingNextPlayer);
    }
}
