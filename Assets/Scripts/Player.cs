using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Singleton Pattern with a property
    public static Player Instance { get; private set; } 

    // extending the EventHandler Generic to send more information
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float interactionRange = 2f;
    private bool isWalking;
    private Vector3 lastInteractDirection;
    private ClearCounter selectedCounter;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("There is more than one Player instance!");
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(selectedCounter != null)
        {
            selectedCounter.Interact();
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }
    private void HandleInteractions()
    {
        // get Player movement Vector
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        // convert x,y to x,z
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDirection = moveDir;
        }

        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHit, interactionRange, counterLayerMask))
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if(clearCounter != selectedCounter)
                {
                    SetSelectedCounter(clearCounter);
                }                
            }
            else
            {
                Debug.Log("first else");
                SetSelectedCounter(null);
            }
        else
        {            
            if (selectedCounter != null)
            {
                Debug.Log("2nd else");
                SetSelectedCounter(null);
            }
                
        }
    }
    private void HandleMovement()
    {
        // get Player movement Vector
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        // convert x,y to x,z
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);


        // check if something is blocking the path the player wants to move to
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        // apply Movement to PlayerModel
        if (!canMove)
        {

            // Attempt only X Movement 
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
                moveDir = moveDirX;
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                    moveDir = moveDirZ;
            }

        }
        if (canMove)
            transform.position += moveDir * moveDistance;
        // movement alternatives
        //transform.eulerAngles = moveDir;
        //transform.LookAt(transform.position);

        // toogle Bool for movement Animator
        isWalking = moveDir != Vector3.zero;

        // Player Rotation via Slerp (not Lerp)
        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }

    private void SetSelectedCounter(ClearCounter selectedCounter)
    {        
        this.selectedCounter = selectedCounter;

        // fire Event that notifies every Counter that the active Counter has changed
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }
}
