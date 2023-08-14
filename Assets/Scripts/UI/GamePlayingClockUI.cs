using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    public static GamePlayingClockUI Instance { get; private set; }

    [SerializeField] private Image clockFilledAmount;

    private void Awake()
    {
        Instance = this;
        ResetClockFillAmount();
    }

    public void ResetClockFillAmount()
    {
        clockFilledAmount.fillAmount = 1f;
    }

    private void Update()
    {
        clockFilledAmount.fillAmount = KitchenGameManager.Instance.GetPlayingTimerNormalized();
    }

}
