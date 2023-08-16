using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyItemTrashed;
    new public static void ResetStaticData()
    {
        OnAnyItemTrashed = null;
    }

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            OnAnyItemTrashed?.Invoke(this, EventArgs.Empty);
            // Delete Object the player is holding            
            player.GetKitchenObject().DestroySelf();
        }
    }
}
