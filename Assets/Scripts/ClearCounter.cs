using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform topCounterPoint;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;

    private KitchenObject kitchenObject;


    private void Update()
    {
        if (testing && Input.GetKeyDown(KeyCode.T) && secondClearCounter != null)
        {
            // put this item on top to another counter            
            if(kitchenObject != null)
            {
                Debug.Log("Testing");
                kitchenObject.SetClearCounter(secondClearCounter);                
            }            
        }
    }

    public void Interact()
    {


        if (kitchenObject == null)
        {
            // for now we spawn the assigned KitchenObjectSO
            Debug.Log("Interact " + transform.name);
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, topCounterPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
        }
        else
        {
            // return the type of KitchenObjectSO that's on the Counter
            Debug.Log("There is an object on this Counter:" + kitchenObject.name);
            Debug.Log("This Counter is" + kitchenObject.GetClearCounter());
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

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
