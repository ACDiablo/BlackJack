using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName =("Create/Cart/Deck"))]
public class Deck : ScriptableObject
{
    public Cart[] deck;
     public List<Cart> runTimeDeck;
}
