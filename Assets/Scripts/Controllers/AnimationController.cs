using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private int animationState;
    private static readonly int _state = Animator.StringToHash("State");
    private static readonly int _aiming = Animator.StringToHash("Aiming");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void UpdateAnimationState(PlayerState state)
    {
        animationState = (int)state;
        animator.SetInteger(_state, animationState);
    }
    private void UpdateAimAnimationState(AimState state)
    {
        animationState = (int)state;
        animator.SetInteger(_aiming, animationState);
    }

    private void OnEnable()
    {
        GameManager.Instance.OnPlayerStateChanged += UpdateAnimationState;
        GameManager.Instance.OnAimStateChanged += UpdateAimAnimationState;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPlayerStateChanged -= UpdateAnimationState;
        GameManager.Instance.OnAimStateChanged -= UpdateAimAnimationState;
    }
}
