using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HintManager : MonoBehaviour
{
    float hintDurationSeconds = 2f;

    public static event Action<int> OnHintStatusCheck;

    private void OnEnable()
    {
        EnemyController.OnPlayerDamaged += ManagementHint;
    }

    private void OnDisable()
    {
        EnemyController.OnPlayerDamaged -= ManagementHint;
    }

    void ManagementHint(int targetId)
    {
        if (!HintStatus.isHintActive(targetId)) //return bool type
        {
            StartCoroutine(ShowHintForSeconds(targetId, hintDurationSeconds));
        }

        else { Debug.Log(targetId.ToString() + " 은 이미 힌트 활성화되었습니다."); }
    }

    IEnumerator ShowHintForSeconds(int targetId, float duration)
    {
        Debug.Log(targetId.ToString()+ " 을 해쉬에 등록합니다");
        HintStatus.Add(targetId);
        OnHintStatusCheck?.Invoke(targetId);

        yield return new WaitForSeconds(duration);

        HintStatus.Remove(targetId);
        OnHintStatusCheck?.Invoke(targetId);
        Debug.Log(targetId.ToString() + "의 힌트가 사라집니다.");
    }
}



    