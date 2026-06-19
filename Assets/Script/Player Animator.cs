using NUnit.Framework.Internal.Commands;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    

    Animator animator;
    private const string  IS_WALKING = "IsWalking";
    [SerializeField] Player Player;

    void Start()
    {
       animator =   GetComponent<Animator>();
       

    }

  



    void Update()
    {
        animator.SetBool(IS_WALKING, Player.IsWalking());
    }
}
