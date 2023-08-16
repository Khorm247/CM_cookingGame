using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clears static fields to avoid reference errors
/// </summary>
public class ResetStaticDataManager : MonoBehaviour
{
    private void Awake()
    {
        CuttingCounter.ResetStaticData();
        TrashCounter.ResetStaticData();
        BaseCounter.ResetStaticData();
    }
}
