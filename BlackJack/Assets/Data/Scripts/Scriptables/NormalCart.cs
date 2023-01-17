using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = ("Create/Cart/NormalCarts"))]

public class NormalCart : Cart
{
    public int cardValue;
    


    public override void AddValue(Player target)
    {
        target.score += cardValue;
        target.score2 += cardValue;
    }

    public override void CardEffect()
    {
        //No effect for normal card
    }
}
