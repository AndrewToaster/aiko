using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Movement
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 moveDirection = Vector2.zero;

    // References
    private Rigidbody2D rb;

    // Animations (rename the consts as they're in the Animator Controller - the filename.anim can be different)
    private Animator animator;
    public string currentAnimState;
    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_LEFT = "Player_Left";
    const string PLAYER_RIGHT = "Player_Right";
    const string PLAYER_UP = "Player_Up";
    const string PLAYER_DOWN = "Player_Down";

    //private InputAction _moveVector;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //_moveVector = InputManager.instance.input.actions.FindAction("Movement", true);
    }

    private void Update()
    {
        CheckInput();
        AnimationHandler();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void CheckInput()
    {
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void Move()
    {
        moveDirection = moveDirection.normalized;
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void AnimationHandler()
    {
        if (moveDirection.x > 0) ChangeAnimationState(PLAYER_RIGHT);
        else if (moveDirection.x < 0) ChangeAnimationState(PLAYER_LEFT);
        else if (moveDirection.y > 0) ChangeAnimationState(PLAYER_UP);
        else if (moveDirection.y < 0) ChangeAnimationState(PLAYER_DOWN);
        else ChangeAnimationState(PLAYER_IDLE);
    }

    private void ChangeAnimationState(string newState)
    {
        if (currentAnimState == newState) return;
        currentAnimState = newState;
        animator.Play(currentAnimState);
    }
}
