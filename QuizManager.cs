using NUnit.Framework.Internal;
using System;
using System.Collections;  
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public static event Action<Vector3> OnAnswerCorrect;

    public EnemyListSO enemyListSO;
    public GameObject resultText;

    //check and comparing answer
    public void CheckAnswer(string playerInput)
    {
        string trimedPlayerInput = playerInput.Trim();

        EnemyData removeTarget = null;

        //check answer in enemy field
        foreach (EnemyData data in enemyListSO.GetEnemyList())
        {
            if (trimedPlayerInput.Equals(data.GetMeaning()))
            {
                //give enemy position info
                Vector3 enemyPos = data.GetEnemyGameObject().transform.position;
                OnAnswerCorrect?.Invoke(enemyPos);


                //destroy only "first correct object" with EnemyController.cs
                EnemyController controller = data.GetEnemyGameObject().GetComponent<EnemyController>();
                controller.isKilledbyPlayer = true;
                controller.Die();
                
                removeTarget = data;
                break; //break foreach loop for destroy only first object
            }
        }

        //remove element from list
        if (removeTarget != null)
        {
            enemyListSO.RemoveEnemyData(removeTarget);
            //Debug.Log("정답!");
            resultText.GetComponent<TextMeshProUGUI>().text = "정답!";
        }

        else //removeTarget is null
        {
            //Debug.Log("오답!");
            resultText.GetComponent<TextMeshProUGUI>().text = "정답이 없습니다!!";
        }
    }//CheckAsnwer(s)
}
