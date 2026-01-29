using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDatta", menuName = "Scriptable Objects/CharacterDatta")]
public class CharacterDatta : ScriptableObject
{
    public string jumpAnimationName ="Jump";
    public string moveAnimationName ="Move"; 
    public string rollAnimationName ="Roll";
    public string loseAnimationName ="Lose";
    public string runAnimationName ="Run";
}
