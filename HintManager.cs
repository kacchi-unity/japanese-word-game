using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HintManager : MonoBehaviour
{
    float hintDurationSeconds = 10f;

    public static event Action<int, HintRenderMode> OnHintStatusCheck;

    public LobbySettingSO lobbySetting;

    public enum HintRenderMode
    {
        Blink,
        Fade
    }

    private void OnEnable()
    {
        EnemyController.OnPlayerDamaged += ManagementHint;
    }

    private void OnDisable()
    {
        EnemyController.OnPlayerDamaged -= ManagementHint;
    }

    void Awake()
    {
        this.hintDurationSeconds = this.lobbySetting.settingValue.GetValue(SettingList.HintActiveTime);
    }

    void ManagementHint(int targetId, float fadeInDuration, float fadeOutDuration, float unused)
    {
        if (!HintStatus.isHintActive(targetId)) //return bool type
        {
            HintRenderMode currentHintRenderMode = this.hintDurationSeconds > fadeInDuration + fadeOutDuration
                ? HintRenderMode.Fade
                : HintRenderMode.Blink;

            StartCoroutine(ShowHintForSeconds(targetId, fadeOutDuration, currentHintRenderMode));
        }

    }

    IEnumerator ShowHintForSeconds(int targetId, float fadeOutDuration, HintRenderMode mode)
    {
        HintStatus.Add(targetId);
        OnHintStatusCheck?.Invoke(targetId, mode);

        yield return new WaitForSeconds(hintDurationSeconds - fadeOutDuration);

        if (mode == HintRenderMode.Fade)
        {
            OnHintStatusCheck?.Invoke(-targetId, mode); //Using a (-) sign for only apply fading effect, Trick the internal function
            yield return new WaitForSeconds(fadeOutDuration);

            HintStatus.Remove(targetId); //Code where the Hint Word ID actually disappears
        }

        else if (mode == HintRenderMode.Blink)
        {
            yield return new WaitForSeconds(fadeOutDuration);

            HintStatus.Remove(targetId);
            OnHintStatusCheck?.Invoke(targetId, mode);
        }
    }
}



    