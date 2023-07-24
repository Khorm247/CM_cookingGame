using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform topCounterPoint;

    private KitchenObject kitchenObject;

    public void Interact(Player player)
    {


        if (kitchenObject == null)
        {
            // for now we spawn the assigned KitchenObjectSO
            Debug.Log("Interact " + transform.name);
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, topCounterPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else
        {

            // and give it to the player
            kitchenObject.SetKitchenObjectParent(player);
        }       
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return topCounterPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() { return kitchenObject; }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
