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

    private void OnEnable()
    {
        WordBoardButtonManager.OnBattleStartButtonClick += FocusInput;
    }

    private void OnDisable()
    {
        WordBoardButtonManager.OnBattleStartButtonClick -= FocusInput;
    }

    void Awake()
    {
        whiteInput.onSubmit.AddListener(BringAnswer);
    }

    void OnDestroy()
    {
        whiteInput.onSubmit.RemoveListener(BringAnswer);
    }

    void FocusInput()
    {
        //focus on input field
        whiteInput.text = "";
        whiteInput.Select();
        whiteInput.ActivateInputField();
    }
}