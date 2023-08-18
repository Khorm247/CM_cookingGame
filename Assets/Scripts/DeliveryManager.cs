using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeDelivered;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 10f;
    private int waitingRecipesMax = 4;
    private int successfulRecipesDeliveredAmount;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
        successfulRecipesDeliveredAmount = 0;
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if(waitingRecipeSOList.Count < waitingRecipesMax ) 
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];                
                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }            
        }
    }

    public bool DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++) 
        { 
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
            
            if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                // same number of ingredients detected
                // check if ingredients are correct
                bool plateContentsMatchesRecipe = true;
                foreach(KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    // cycling through all ingredients in the recipe
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        // cycling through all ingredients on the plate
                        if(plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            // ingredient matches!
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        // Failstate!
                        // This Recipe ingredient was not found on the plate
                        plateContentsMatchesRecipe = false;
                        break;
                    }
                }

                if (plateContentsMatchesRecipe)
                {
                    // correct Recipe delivered!
                    Debug.Log("Recipe delivered!");
                    waitingRecipeSOList.RemoveAt(i);

                    OnRecipeDelivered?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    successfulRecipesDeliveredAmount++;
                    return true;
                }
            }
        }
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        return false;
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetSuccessfulRecipesDeliveredAmount()
    {
        return successfulRecipesDeliveredAmount;
    }

    private void OnDestroy()
    {
        //Debug.Log("I got destroyed! " + this.gameObject.name);
    }
}
