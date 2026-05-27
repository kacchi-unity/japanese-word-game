using TMPro;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WordSearchManager : MonoBehaviour
{
    [Header("Script Connect")]
    public SwordRecordSO swordRecordSO;
    public WordDataBaseSO wordDataBaseSO;

    [Header("UI Connect")]
    public TMP_InputField inputSearch;
    public TextMeshProUGUI searchResultLabel;
    public Button searchEnterButton;
    public Button searchRemoveButton;
    

    private bool isProcessing = false;

    private Dictionary<string, List<int>> swordRecordDic = null;
    private List<int> userSearchWordIdList = null;

    public static event Action<List<int>, string> OnUserSearched;

    private void OnEnable()
    {
        searchRemoveButton.onClick.AddListener(ClickRemoveButton);
        searchEnterButton.onClick.AddListener(ClickSearchEnterButton);
    }

    private void OnDisable()
    {
        searchRemoveButton.onClick.RemoveListener(ClickRemoveButton);
        searchEnterButton.onClick.RemoveListener(ClickSearchEnterButton);
    }

    void Awake()
    {
        inputSearch.onSubmit.AddListener(SearchWord);
    }

    void OnDestroy()
    {
        inputSearch.onSubmit.RemoveListener(SearchWord);
    }

    void Start()
    {
        List<int> swordRecordList = swordRecordSO.GetSwordRecordList();
        swordRecordDic = new Dictionary<string, List<int>>(swordRecordList.Count);
        Dictionary<int,Word> wordDataBase = wordDataBaseSO.GetWordDataBase();

        foreach (int wordId in swordRecordList)
        {
            if (!wordDataBase.ContainsKey(wordId))
            {
                continue;
            }

            string wordMeaning = wordDataBase[wordId].meaning;

            if(!this.swordRecordDic.ContainsKey(wordMeaning))
            {
                this.swordRecordDic.Add(wordMeaning, new List<int>());
            }

            //같은 한글 뜻 meaning, but 다른 고유 한자 또는 ID인 wordId Add 처리
            if (!this.swordRecordDic[wordMeaning].Contains(wordId))
            {
                this.swordRecordDic[wordMeaning].Add(wordId);
            }
        }

        //Label init setting
        searchResultLabel.text = "";
    }

    void SearchWord(string text)
    {
        if (isProcessing)
        {
            return;
        }

        if (string.IsNullOrEmpty(text))
        {
            return;
        }

        //Block process enter key
        isProcessing = true;

        Debug.Log($"사용자 입력: {text}");

        MakeMatchingList(text);

        SendMachingList(this.userSearchWordIdList, text);

        StartCoroutine(FocusInputAndUnlock());
    }

    void MakeMatchingList(string userSearchText)
    {
        this.userSearchWordIdList = new List<int>();

        //Data cleaning
        string cleanedUserSearchText = userSearchText.Replace(" ","").ToLower();
        if (this.swordRecordDic == null || this.swordRecordDic.Count == 0)
        {
            Debug.LogWarning($"{swordRecordDic}가 정의되지 않았습니다.");
            return;
        }

        foreach (var targetData in this.swordRecordDic)
        {
            string cleanedTargetDataMeaning = targetData.Key.Replace(" ","").ToLower();

            if (cleanedTargetDataMeaning.Contains(cleanedUserSearchText))
            {
                foreach (int findWordId in targetData.Value)
                {
                    this.userSearchWordIdList.Add(findWordId);
                }
            }
        }
    }

    void SendMachingList(List<int> sendTargetList, string keyWord)
    {
        if (sendTargetList == null || sendTargetList.Count == 0)
        {
            searchResultLabel.text = "검색 결과가 없습니다.";
            OnUserSearched?.Invoke(null, null);
            return;
        }

        searchResultLabel.text = "";
        OnUserSearched?.Invoke(sendTargetList, keyWord.Replace(" ", "").ToLower());
    }

    void ClickRemoveButton()
    {
        OnUserSearched?.Invoke(this.swordRecordSO.GetSwordRecordList(), null);
        inputSearch.text = "";
        searchResultLabel.text = "";
    }

    IEnumerator FocusInputAndUnlock()
    {
        yield return new WaitForSeconds(0.75f);
        //focus on input field
        if(inputSearch != null)
        {
            inputSearch.Select();
            inputSearch.ActivateInputField();
        }

        isProcessing = false;
    }

    void ClickSearchEnterButton()
    {
        SearchWord(this.inputSearch.text);
    }
}
