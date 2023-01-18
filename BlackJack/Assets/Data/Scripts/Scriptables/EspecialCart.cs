using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = ("Create/Cart/EspecialCarts"))]
public class EspecialCart : Cart
{
    public override void AddValue(Player target)
    {
        //No score add value
    }

    public override void CardEffect()
    {
        //TO do Program all variants
    }

    public override int GetCardValue()
    {
        throw new System.NotImplementedException();
    }

    public override Vector2 GetCardValues()
    {
        throw new System.NotImplementedException();
    }
}
