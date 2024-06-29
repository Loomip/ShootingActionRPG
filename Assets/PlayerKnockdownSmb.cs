using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockdownSmb : StateMachineBehaviour
{
    [SerializeField] private float knockBackSize;
    [SerializeField] private float knockBackDuration = 0.7f;

    private float time;
    private Vector3 backward;
    private CharacterController characterController;
    private CharacterInputMovement inputMovement;
    private CharacterAttackComponent attackComponent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        backward = -animator.transform.forward;
        animator.applyRootMotion = false;

        // 타이머 초기화
        time = 0f;

        characterController = animator.GetComponent<CharacterController>();
        inputMovement = animator.GetComponent<CharacterInputMovement>();
        attackComponent = animator.GetComponent<CharacterAttackComponent>();

        if (characterController == null)
        {
            Debug.LogError("CharacterController component not found on Player");
        }
        inputMovement.enabled = false;
        attackComponent.enabled = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time += Time.deltaTime;

        if (characterController != null)
        {
            if (time < knockBackDuration)
            {
                // Apply knockback force to the player's CharacterController
                characterController.Move(backward * knockBackSize * Time.deltaTime);
            }
            else
            {
                // Stop movement after knockback duration
                characterController.Move(Vector3.zero);
            }
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        inputMovement.enabled = true;
        attackComponent.enabled = true;
    }
}
