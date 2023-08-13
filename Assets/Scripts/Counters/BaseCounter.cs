using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {
   
    [SerializeField] private Transform topCounterPoint;

    private KitchenObject kitchenObject;

    public virtual void Interact(Player player)
    {        
        Debug.Log("Basecounter.Interact();");
    }

    public virtual void InteractAlternate(Player player)
    {
        Debug.Log("Basecounter.InteractAlternate();");
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
