using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SelectWordProcessManager : MonoBehaviour
{
    HashSet<int> selectedWordIdHash = new HashSet<int>();
    //To cancel subscription
    List<WordCardSetting> activeCards = new List<WordCardSetting>();
    public static event Action<int> isClickStartButton;
    public Button startButton;

    private void OnEnable()
    {
        WordCardSetting.OnCardSpawned += RegisterSpawnedCard;
    }

    private void OnDisable()
    {
        WordCardSetting.OnCardSpawned -= RegisterSpawnedCard;

        foreach (var card in activeCards)
        {
            if (card != null)
            {
                card.OnToggleChanged -= HandleWordSelected;
            }
        }
        activeCards.Clear();
    }

    //Button connection
    void Awake()
    {
        startButton.onClick.AddListener(this.OnStartButtonClick);
    }

    void OnDestroy()
    {
        startButton.onClick.RemoveListener(this.OnStartButtonClick);
    }

    private void RegisterSpawnedCard(WordCardSetting targetCard, int wordId)
    {
        targetCard.OnToggleChanged += HandleWordSelected;

        activeCards.Add(targetCard);
    }

    public void HandleWordSelected(int wordId, bool isSelected)
    {
        if (isSelected)
        {
            this.selectedWordIdHash.Add(wordId);
            Debug.Log($"해쉬 삽입 ID{wordId}, 불린 여부 {isSelected} ");
        }

        else
        {
            this.selectedWordIdHash.Remove(wordId);
            Debug.Log($"해쉬 제거 ID{wordId}, 불린 여부 {isSelected} ");
        }
    }

    public void OnStartButtonClick() 
    {
        if (selectedWordIdHash.Count > 0)
        {SceneTracker.selectorSwordRecordWordList = new List<int>(this.selectedWordIdHash);
            SceneTracker.previousScene = SceneTracker.SceneType.SwordRecord;
        }

    isClickStartButton?.Invoke(selectedWordIdHash.Count);
    }
}
