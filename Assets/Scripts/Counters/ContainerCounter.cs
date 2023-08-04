using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // there is no KitchenObject
            if (player.HasKitchenObject())
            {
                // Player is carrying something
                // so the player drops it on the container                
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // Spawn Item for the Player to pickup                
                SpawnKitchenObjectAndGiveItToPlayer(player);
            }
        }
        else
        {
            // there IS a KitchenObject
            if (!player.HasKitchenObject())
            {                
                // Player is free to pick up the counter object
                GetKitchenObject().SetKitchenObjectParent(player);                          
            }
            else
            {
                // Player has an object
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // player is holding a plate                    
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
        }
    }

    /// <summary>
    /// Spawn Object and give it to the player
    /// </summary>
    /// <param name="player"></param>
    /// <param name=""></param>
    private void SpawnKitchenObjectAndGiveItToPlayer(Player player)
    {
        Debug.Log("Spawning object");
        KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);        
    }
}
