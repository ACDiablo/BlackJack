using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName =("Create/Cart"))]
public abstract class Cart : ScriptableObject
{
    public CartType type;
    public Sprite cardSprite;
    public string cardName;

    public abstract void AddValue(Player target);
    public abstract int GetCardValue();
    public abstract Vector2 GetCardValues();

    public abstract void CardEffect();
    
    
}
public enum CartType{ Normal,As ,Action}
