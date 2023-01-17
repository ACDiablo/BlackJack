using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Player : MonoBehaviour
{
    public List<Cart> carts;
    public int score;
    public int score2;
    public int money;
    public int idPlayer;
    public bool isIA;
    public Player(int _money, int _idPlayer, bool _isIA)
    {
        score = 0;
        score2 = 0;
        money = _money;
        idPlayer = _idPlayer;
        isIA = _isIA;
    }


}
