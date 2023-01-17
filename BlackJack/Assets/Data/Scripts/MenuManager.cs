using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject createGamePanel, rulesPanel;
    public void Play()
    {
        createGamePanel.SetActive(true);
        gameObject.SetActive(false);
    }
    public void Rules()
    {
        rulesPanel.SetActive(true);
    }
}
