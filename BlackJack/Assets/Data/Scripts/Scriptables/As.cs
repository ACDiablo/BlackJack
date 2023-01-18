using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName =("Create/Cart/As"))]
public class As : Cart
{
    public Vector2 values;
    public override void AddValue(Player target)
    {
        target.score += (int)values.x;
        if(target.score2+values.y>21)
        {
            target.score2 += (int)values.x;
        }
        else
            target.score2 += (int)values.y;
    }

    public override void CardEffect()
    {
        throw new System.NotImplementedException();
    }

    public override int GetCardValue()
    {
        throw new System.NotImplementedException();
    }

    public override Vector2 GetCardValues()
    {
        return values;
    }
}
