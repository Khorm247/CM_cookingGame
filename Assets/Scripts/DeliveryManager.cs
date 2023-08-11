using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;

    public List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if(waitingRecipeSOList.Count < waitingRecipesMax ) 
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSO.recipeName);
                waitingRecipeSOList.Add(waitingRecipeSO);
            }            
        }
    }

    public bool DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++) 
        { 
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
            //Debug.Log("waitingRecipeSO.kitchenObjectSOList:");
            //foreach (KitchenObjectSO koso in waitingRecipeSO.kitchenObjectSOList)
            //    Debug.Log(koso);

            //Debug.Log("plateKitchenObject.GetKitchenObjectSOList()");
            //foreach (KitchenObjectSO koso in plateKitchenObject.GetKitchenObjectSOList())
            //    Debug.Log(koso);
            
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
                    return true;
                }
            }
        }
        return false;
    }
}
