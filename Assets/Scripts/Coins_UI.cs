using UnityEngine;
using UnityEngine.UI;
public class Coins_UI : MonoBehaviour
{
 [SerializeField]
 private Text coinsText;
 [SerializeField]
 private Animator animator;
 [SerializeField]
 private string animationName = "Wiggle";
 public void UnpdateCoinsText(string coins)
 {
    animator.Play(animationName, 0, 0f);
    coinsText.text = coins;
 }
}
