using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {        
        if (!HasKitchenObject())
        {
            // there is no KitchenObject
            if (player.HasKitchenObject())
            {
                // Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // player has nothing
            }
        }
        else
        {
            // there IS a KitchenObject
            if (player.HasKitchenObject())
            {
                // Player has an object
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // player is holding a plate                    
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }                    
                }
            }
            else
            {
                // Player is free to pick up the counter object
                GetKitchenObject().SetKitchenObjectParent(player);                
            }
        }
    }
}
