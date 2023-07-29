using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
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
                // Player has an object but should not be allowed to drop it
            }
            else
            {
                // Player is free to pick up the counter object
                GetKitchenObject().SetKitchenObjectParent(player);
                Debug.Log("setting object to player");
            }
        }
    }
}
