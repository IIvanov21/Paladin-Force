using UnityEngine;

[CreateAssetMenu(menuName ="Dialogue/Line")]
public class DialogueLine:ScriptableObject
{
    public string text;
    public DialogueLine nextLine;


}
