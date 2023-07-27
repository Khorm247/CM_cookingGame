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
                Debug.Log("no object on counter, player drops his object");
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // Spawn Item for the Player to pickup
                Debug.Log("no object on counter, player was not holding anything");
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
                Debug.Log("setting object to player");                
            }
            else
            {
                // Player has an object but should not be allowed to drop it
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
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);        
    }
}
