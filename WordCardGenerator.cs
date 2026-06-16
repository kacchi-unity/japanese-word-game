using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class WordCardGenerator : MonoBehaviour
{
    [Header("Script Connect")]
    public SwordRecordSO swordRecordSO;
    public WordCardSetting wordCardPrefab;
    
    [Header("UI Connect")]
    public RectTransform content;
    public Button startButton;
    public TextMeshProUGUI noneMessageText;
    public ScrollRect scrollRect;

    [Header("Setting Value")]
    private float wordCardWidth;
    [SerializeField] private float horizontalSpacing = 100;
    [SerializeField] private int verticalSlots = 3;
    [SerializeField] private int visibleHorizontalCount = 5;

    [Header("Data Set")]
    private List<int> swordRecordList = null;
    private Dictionary<int, Word> wordDataBase = null;
    private List<WordCardSetting> wordCardPool = new List<WordCardSetting>();

    [Header("[ 인터페이스 설정 ]")]
    [Tooltip("ICheckable 인터페이스를 상속받은 오브젝트를 넣어주세요.")]
    [SerializeField] private GameObject wordcheckerObject;

    private ICheckable wordCheckerInterface; //Interface

    private int prevStartColumn = -1;

    private List<int> currentTargetWordIdList = null;

    private string searchKeyWord = null;


    private void Awake()
    {
        scrollRect.onValueChanged.AddListener(OnScrollMoving);

        //Interface connect check
        if (wordcheckerObject != null)
        {
            wordCheckerInterface = wordcheckerObject.GetComponent<ICheckable>();
            
            if (wordCheckerInterface == null)
            {
                Debug.LogWarning($"{gameObject.name}내 넣은 오브젝트에 ICheckable 상속 스크립트가 없습니다!");
            }
        }
    }

    private void OnDestroy()
    {
        scrollRect.onValueChanged.RemoveListener(OnScrollMoving);
    }

    private void OnEnable()
    {
        WordSearchManager.OnUserSearched += HandleUserSearchEvent;
    }

    private void OnDisable()
    {
        WordSearchManager.OnUserSearched -= HandleUserSearchEvent;
    }

    void HandleUserSearchEvent(List<int> searchedWordId, string keyWord)
    {
        this.searchKeyWord = keyWord;
        PrintVisibleWordCards(searchedWordId);
    }

    void Start()
    {
        this.swordRecordList = swordRecordSO.GetSwordRecordList();
        this.wordDataBase = WordLoader.Instance.GetWordDataBase();

        //Safety button interacte logic
        startButton.interactable = false;

        noneMessageText.text = null;

        if (swordRecordList == null || wordDataBase == null)
        {
            Debug.LogWarning("List 또는 Dictinary가 할당되지 않았습니다. 로직을 중단합니다.");
            return;
        }

        if (wordDataBase.Count == 0)
        {
            Debug.LogWarning("Dictionary Elenemy 개수가 0입니다. 로직을 중단합니다.");
            return;
        }

        if (swordRecordList.Count == 0)
        {
            noneMessageText.text = "전투를 시작하여 단어를 모아보세요!";
            return;
        }

        //Value init setting
        this.wordCardWidth = wordCardPrefab.RectTransform.rect.width;

        InitSetting();

        PrintVisibleWordCards(this.swordRecordList);

        //Safety button interacte logic
        startButton.interactable = true;
    }

    void InitSetting()
    {
        foreach (Transform child in this.content)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < visibleHorizontalCount * verticalSlots; i++)
        {
            WordCardSetting prefab = Instantiate(this.wordCardPrefab, this.content);
            prefab.gameObject.SetActive(false);

            wordCardPool.Add(prefab);
        }
    }

    void PrintVisibleWordCards(List<int> targetWordIdList)
    {
        if (targetWordIdList == null || targetWordIdList.Count == 0)
        {
            this.currentTargetWordIdList = targetWordIdList;

            SetContentSize(0);

            for (int i = 0; i < wordCardPool.Count; i++)
            {
                if (wordCardPool[i] != null)
                {
                    wordCardPool[i].gameObject.SetActive(false);
                }
            }
            return;
        }

        if (this.currentTargetWordIdList != targetWordIdList)
        {
            prevStartColumn = -1;
        }

        this.currentTargetWordIdList = targetWordIdList;

        SetContentSize(targetWordIdList.Count);

        float scrollX = Mathf.Abs(content.anchoredPosition.x);
        int startColumn = Mathf.FloorToInt(scrollX / (wordCardWidth + horizontalSpacing));

        if (prevStartColumn == startColumn)
        {
            return;
        }

        prevStartColumn = startColumn;

        for (int i = 0; i < wordCardPool.Count; i++)
        {
            int dataIndex = (startColumn * verticalSlots) + i;
            WordCardSetting visibleWordCard = wordCardPool[i];

            if (dataIndex >= 0 && dataIndex < targetWordIdList.Count)
            {
                visibleWordCard.gameObject.SetActive(true);

                int targetWordId = targetWordIdList[dataIndex];

                //Highlight meaning text with search text if searchKeyWord is exist
                string wordMeaning = wordDataBase[targetWordId].meaning;

                if (this.searchKeyWord != null)
                {
                    wordMeaning = GetHighlightedText(wordMeaning, this.searchKeyWord);
                }

                visibleWordCard.SetData(
                    wordDataBase[targetWordId].kanji,
                    wordMeaning,
                    swordRecordSO.GetCorrectRate(targetWordId),
                    targetWordId,
                    this.wordCheckerInterface.IsWordSelected(targetWordId)
                    );

                RectTransform wordCardRT = visibleWordCard.RectTransform;

                int columnIndex = dataIndex / verticalSlots;
                int rowIndex = dataIndex % verticalSlots;

                float anchoredX = columnIndex * (wordCardWidth + horizontalSpacing);
                float anchoredY = content.sizeDelta.y * ((verticalSlots - rowIndex - 0.5f) / verticalSlots);

                wordCardRT.anchoredPosition = new Vector2(anchoredX, anchoredY);
            }

            else
            {
                visibleWordCard.gameObject.SetActive(false);
            }
        }
    } //PrintVisibleWordCards(List<int>)


    //Method overloading
    void PrintVisibleWordCards()
    {
        //First processing
        if (this.currentTargetWordIdList == null || this.currentTargetWordIdList.Count == 0)
        {
            return;
        }

        //Second processing
        if (this.currentTargetWordIdList != null && this.currentTargetWordIdList.Count > 0)
        {
            PrintVisibleWordCards(this.currentTargetWordIdList);
            return;
        }

        //Thrid processing
        if (this.swordRecordList != null && this.swordRecordList.Count > 0)
        {
            PrintVisibleWordCards(this.swordRecordList);
        }
    }

    void SetContentSize(int listCount)
    {
        if (listCount == 0)
        {
            content.sizeDelta = new Vector2(0, content.sizeDelta.y);
        }

        int totalColumn = (listCount + verticalSlots - 1) / verticalSlots; //Ceiling process
        float contentWidth = ((totalColumn * wordCardWidth) + ((totalColumn - 1) * horizontalSpacing));
        if (contentWidth < 0)
        {
            contentWidth = 0;
        }
        content.sizeDelta = new Vector2(contentWidth, content.sizeDelta.y);
    }

    void OnScrollMoving(Vector2 unused)
    {
        PrintVisibleWordCards();
    }
    
    //Highlight text handling
    private string GetHighlightedText(string originalText, string searchKeyWord)
    {
        searchKeyWord = searchKeyWord.Replace(" ", "").ToLower();

        string pattern = string.Join(@"\s*", searchKeyWord.Select(c => Regex.Escape(c.ToString())));

        try
        {
            return Regex.Replace(originalText, pattern, $"<mark=#FFFF00AA>$&</mark>", RegexOptions.IgnoreCase);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"강조 로직 에러: {e.Message}");
            return originalText;
        }
    }
}