using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            // Delete Object the player is holding            
            player.GetKitchenObject().DestroySelf();
        }
    }
}
