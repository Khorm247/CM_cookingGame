using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform topCounterPoint;
    public void Interact()
    {
        Debug.Log("Interact " + transform.name);
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, topCounterPoint);
        kitchenObjectTransform.localPosition = Vector3.zero;

        Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().name);
    }
}
