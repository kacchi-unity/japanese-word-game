using System.Collections;  
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NUnit.Framework.Internal;
using System;

public class EnemyListManager : MonoBehaviour
{
    public static event Action<Vector3> OnAnswerCorrect;

    public List<EnemyData> EnemyList;
    GameObject resultText;

    //check and comparing answer
    public void CheckAnswer(string playerInput)
    {
        string trimedPlayerInput = playerInput.Trim();

        EnemyData removeTarget = null;

        //check answer in enemy field
        foreach (EnemyData data in EnemyList)
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
            RemoveEnemyData(removeTarget);
            //Debug.Log("정답!");
            resultText.GetComponent<TextMeshProUGUI>().text = "정답!";
        }

        else //removeTarget is null
        {
            //Debug.Log("오답!");
            resultText.GetComponent<TextMeshProUGUI>().text = "정답이 없습니다!!";
        }
    }//CheckAsnwer(s)

    //add enemy data to list (from generator script)
    public void AddEnemyData(EnemyData data)
    {
        EnemyList.Add(data);
        //Debug.Log("리스트 추가 성공");
    }

    //remove enemy data from list
    public void RemoveEnemyData(EnemyData data)
    {
        EnemyList.Remove(data);
        //Debug.Log("가정 첫번째 객체를 삭제함과 동시에 리스트에서 제거완료했습니다!");
    }

    void Awake()
    {
        EnemyList = new List<EnemyData>();
    }

    void Start()
    {
        Application.targetFrameRate = 60;

        resultText = GameObject.Find("Result");

    }

    void Update()
    {
        
    }

}
