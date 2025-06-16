using TMPro;
using UnityEngine;

public class DialogueRunner : MonoBehaviour
{
    public DialogueLine startingLine;
    public TMP_Text dialogueText;

    private DialogueLine currentLine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLine = startingLine;
        ShowCurrentLine();
    }

    public void ShowCurrentLine()
    {
        if(currentLine != null)
        {
            dialogueText.text = currentLine.text;
        }
    }

    public void Next()
    {
        if(currentLine != null)
        {
            currentLine=currentLine.nextLine;
            ShowCurrentLine();
        }
    }
}
