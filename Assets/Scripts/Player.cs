using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float moveSpeed = 7f;
    private bool isWalking;


    private void Update()
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
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
                moveDir = moveDirX;
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                    moveDir = moveDirZ;
            }

        }
        if(canMove)
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

    public bool IsWalking()
    {
        return isWalking;
    }
}
