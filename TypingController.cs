using UnityEngine;
using TMPro;

public class TypingController : MonoBehaviour
{
    public TMP_InputField whiteInput;
    public QuizManager quizManager; 

    void BringAnswer(string text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            quizManager.CheckAnswer(text);
        }

        //clear text and re-focus on input field
        FocusInput();
    }

    void Start()
    {
        FocusInput();
        whiteInput.onSubmit.AddListener(BringAnswer);
    }

    void FocusInput()
    {
        //focus on input field
        whiteInput.text = "";
        whiteInput.Select();
        whiteInput.ActivateInputField();
    }
}