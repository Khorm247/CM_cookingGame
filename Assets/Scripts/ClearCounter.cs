using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private Transform tomatoPrefab;
    [SerializeField] private Transform topCounterPoint;
    public void Interact()
    {
        Debug.Log("Interact " + transform.name);
        Transform tomatoTransform = Instantiate(tomatoPrefab, topCounterPoint);
        tomatoTransform.localPosition = Vector3.zero;
    }
}
