using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;
    [SerializeField] private Vector3 moveDirection;
    private CharacterController controller;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameState.Lose || GameManager.Instance.CurrentGameState == GameState.Win)
        {
            return;
        }
        CalculateMovement();
        UpdateAnimation();
    }

    private void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
       
    }

    private void UpdateAnimation()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            GameManager.Instance.ChangePlayerState(PlayerState.Running);
        }
        else
        {
            GameManager.Instance.ChangePlayerState(PlayerState.Idle);
        }
    }
}
