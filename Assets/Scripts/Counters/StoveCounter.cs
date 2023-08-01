using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;

    public override void Interact(Player player)
    {
        // Player puts on cookable object (for now just meat)
        // cooking process starts (raw -> cooked -> burnt)

        if (!HasKitchenObject())
        {
            // there is no KitchenObject in the pan
            if (player.HasKitchenObject())
            {
                // Player is carrying something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // Player has something that can be fried
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    Debug.Log("TODO: Frying starts here");
                }                                
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
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO cuttingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {        
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
                return fryingRecipeSO.output;
        }
        return null;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
                return fryingRecipeSO;
        }
        return null;
    }
}
