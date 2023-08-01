using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particleGameObject;

    public void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        if (e.state == StoveCounter.State.Idle)        
            Hide();        
        else
            Show();
    }

    public void Show()
    {
        stoveOnGameObject.SetActive(true);
        particleGameObject.SetActive(true);
    }

    public void Hide()
    {
        stoveOnGameObject.SetActive(false);
        particleGameObject.SetActive(false);
    }
}
