using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    
    // Movement variable
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    private bool isWalking;

    public float PlayerRedius = .7f;
    public float PlayerHight = 2f;
    

    [SerializeField] private PLayerGameInput gameInput;

    
    //Input Variable

    public float inractDistance = 2f;
    Vector3 lastIntractDir;
    [SerializeField] LayerMask layerMask;




    void Start()
    {
        gameInput.OnIntractAction += GameInput_OnInteractAction;
    }
    

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        HandleIntraction();
    }

    void Update()
    {
        HandleMovement();
        
    }




    /*
    * Handleing Inputs
    */




    private void HandleIntraction()
    {   
        Vector2 inputVector = gameInput.GetMovementVectoNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0 , inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastIntractDir = moveDir;
        }
        
        float intractDistance = 2f;

        Debug.DrawRay(transform.position + Vector3.up, lastIntractDir * intractDistance ,Color.red);



        
        if (Physics.Raycast(transform.position + Vector3.up, lastIntractDir ,out RaycastHit raycastHit, intractDistance,layerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }
        }

            
        


    }








    /*
    * Handleing Movement Logics and all
    */

    

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectoNormalized();

        Vector3 MoveDir = new Vector3(inputVector.x, 0, inputVector.y);

        float MoveDistance = moveSpeed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHight, PlayerRedius, MoveDir, MoveDistance);
        if (!canMove)
        {
            //When player try to move in direction X
            Vector3 moveDirX = new Vector3(MoveDir.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHight, PlayerRedius, moveDirX, MoveDistance);
            if (canMove)
            {
                MoveDir = moveDirX;
            }
            else
            {
                //when player ty to move z
                Vector3 moveDirZ = new Vector3(0f, 0f, MoveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHight, PlayerRedius, moveDirZ, MoveDistance);

                if (canMove)
                {
                    MoveDir = moveDirZ;
                }
            }

        }

        if (canMove)
        {
            transform.position = transform.position + (MoveDir * MoveDistance);
        }

        isWalking = MoveDir != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, MoveDir, Time.deltaTime * rotateSpeed);
    }


    public bool IsWalking()
    {
        return isWalking;
    }
}
