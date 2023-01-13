using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName =("Create/Cart"))]
public abstract class Cart : ScriptableObject
{
   
    public abstract void AddValue(GameObject target);

    public abstract void CardEffect();
    
    
}
