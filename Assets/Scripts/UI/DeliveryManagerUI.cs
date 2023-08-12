using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManagerInstance_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeDelivered += DeliveryManagerInstance_OnRecipeDelivered;

        UpdateVisual();
    }

    private void DeliveryManagerInstance_OnRecipeDelivered(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManagerInstance_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        // clear existing containers
        foreach (Transform child in container)
        {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        // cycle through the waiting recipes and create containers for them
        foreach(RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            // create UI-Container
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
    }
}
