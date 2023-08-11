using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{    
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                // only accepts plates                

                if (DeliveryManager.Instance.DeliverRecipe(plateKitchenObject))
                {                    
                    Debug.Log("DeliveryCounter: accepted recipe");
                }
                else
                {
                    Debug.Log("DeliveryCounter: WRONG recipe!");
                }
                player.GetKitchenObject().DestroySelf();
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        Debug.Log("No alternate Interaction");        
    }
}
