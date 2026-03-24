using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypingController : MonoBehaviour
{
    public TMP_InputField whiteInput;
    GameObject enemyListManager;

    void BringAnswer()
    {
        string playerInput = whiteInput.text;
        enemyListManager.GetComponent<EnemyListManager>().CheckAnswer(playerInput);

        //clear text and re-focus on input field
        whiteInput.text = "";
        whiteInput.Select();
        whiteInput.ActivateInputField();
    }

    void Start()
    {
        enemyListManager = GameObject.Find("EnemyListManager");

        //focus on input field
        whiteInput.Select();
        whiteInput.ActivateInputField();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            BringAnswer();
        }
    }

    
}