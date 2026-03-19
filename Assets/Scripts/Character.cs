using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.Events;
public class Character : MonoBehaviour
{
    private Rigidbody characterRigidbody;
    [SerializeField]
    private CharacterDatta characterDatta;
    [SerializeField]
    private Animator characterAnimator;
    [SerializeField]
    private float jumpForce = 5f;
    public float JumpForce
    {
        get {return jumpForce; }
        set {jumpForce = value; }
    }
    [SerializeField]
    private float distanceToMove = 2f;
    [SerializeField]
    private float moveDuration=0.2f;
    [SerializeField]
    private Transform characterStartPivot;
    [SerializeField]
    private UnityEvent onJump;
    [SerializeField]
    private UnityEvent onMoveToside;
    [SerializeField]
    private UnityEvent onRoll;
    [SerializeField]
    private Collider normalCollider;
    [SerializeField]
    private Collider rollCollider;
    private bool isGrounded = true;
    private bool isMoving = false;
    private bool isRolling = false;
    private bool isActive = false;
    private void Awake()
    {
        characterRigidbody=GetComponent<Rigidbody>();
    }
    public void StartGame()
    {
        normalCollider.enabled=true;
        rollCollider.enabled = false;
        isRolling = false;
        isMoving = false;
        isActive = true;
        characterAnimator.Play(characterDatta.jumpAnimationName, 0, 0f);
        transform.position = characterStartPivot.position;
    }

    public void Lose()
    {
        isActive = false;
        StopAllCoroutines();
        characterAnimator.Play(characterDatta.loseAnimationName, 0, 0f);
    }
    public void Jump()
    {
        if (!isActive) return;
        if (isGrounded)
        {
            onJump?.Invoke();
            characterAnimator.Play(characterDatta.jumpAnimationName, 0, 0f);
            characterRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        
    }
    public void MoveDown()
    {
        if (!isActive || isRolling) return;
        if (isGrounded)
        {
            characterRigidbody.AddForce(Vector3.down * jumpForce * 2, ForceMode.Impulse);
        }
        characterAnimator.Play(characterDatta.rollAnimationName, 0 , 0f);
        onRoll?.Invoke();
        isRolling = true;
        normalCollider.enabled = false;
        rollCollider.enabled = true;
        StartCoroutine(ResetRoll());
    }
    public void MoveLeft()
    {
        if (transform.position.x <= -distanceToMove) return;
        Move(Vector3.left);
    
    }
    public void MoveRight()
    {
        if (transform.position.x >= distanceToMove) return;
        Move(Vector3.right);
    }
    private void Move(Vector3 direction)
    {
        if (isMoving || !isActive) return;
        onMoveToside?.Invoke();
        characterAnimator.Play(characterDatta.moveAnimationName, 0, 0f);
        isMoving = true;
        Vector3 targetPosition = transform.position + direction * distanceToMove;

        transform.DOMove(targetPosition,moveDuration).SetEase(Ease.OutQuad).OnComplete(() => {isMoving = false;});
    }
    private IEnumerator ResetRoll()
    {
        yield return null;
        yield return new WaitForSeconds(characterAnimator.GetCurrentAnimatorStateInfo(0).length);
        isRolling = false;
        normalCollider.enabled = true;
        rollCollider.enabled = false;
    }    
    public void OnCollisionEnter(Collision collision)
    {
       if (isActive && collision.gameObject.CompareTag("Ground"))
        {
            if (!isRolling)
            {
                characterAnimator.Play(characterDatta.runAnimationName, 0, 0f);    
            }
            isGrounded = true;
        } 
    }

}
