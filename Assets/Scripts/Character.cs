using UnityEngine;
using DG.Tweening;
using System.Collections;
public class Character : MonoBehaviour
{
    private Rigidbody characterRigidbody;
    [SerializeField]
    private CharacterDatta characterDatta;
    [SerializeField]
    private Animator characterAnimator;
    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    private float distanceToMove = 2f;
    [SerializeField]
    private float moveDuration=0.2f;
    private bool isGrounded = true;
    private bool isMoving = false;
    private bool isRolling = false;
    private void Start()
    {
        characterRigidbody = GetComponent<Rigidbody>();
    }

    public void Jump()
    {
        if (isGrounded)
        {
            characterAnimator.Play(characterDatta.jumpAnimationName, 0, 0f);
            characterRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        
    }
    public void MoveDown()
    {
        if (isGrounded)
        {
            characterRigidbody.AddForce(Vector3.down * jumpForce * 2, ForceMode.Impulse);
        }
        characterAnimator.Play(characterDatta.rollAnimationName, 0 , 0f);
        isRolling = true;
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
        if (isMoving) return;
        characterAnimator.Play(characterDatta.moveAnimationName, 0, 0f);
        isMoving = true;
        Vector3 targetPosition = transform.position + direction * distanceToMove;

        transform.DOMove(targetPosition,moveDuration).SetEase(Ease.OutQuad).OnComplete(() => {isMoving = false;});
    }
    private IEnumerator ResetRoll()
    {
        yield return new WaitForSeconds(characterAnimator.GetCurrentAnimatorStateInfo(0).length);
        isRolling = false;
    }    
    public void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.CompareTag("Ground"))
        {
            if (!isRolling)
            {
                characterAnimator.Play(characterDatta.runAnimationName, 0, 0f);    
            }
            isGrounded = true;
        } 
    }

}
