using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class WordCardGenerator : MonoBehaviour
{
    [Header("Script Connect")]
    public SwordRecordSO swordRecordSO;
    public WordDataBaseSO wordDataBaseSO;
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

    private int prevStartColumn = -1;

    [Header("[ 인터페이스 설정 ]")]
    [Tooltip("ICheckable 인터페이스를 상속받은 오브젝트를 넣어주세요.")]
    [SerializeField] private GameObject wordcheckerObject;

    private ICheckable wordCheckerInterface; //Interface

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

    void Start()
    {
        this.swordRecordList = swordRecordSO.GetSwordRecordList();
        this.wordDataBase = wordDataBaseSO.GetWordDataBase();

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

        //Safety button interacte logic
        startButton.interactable = true;
    }

    void InitSetting()
    {
        foreach (Transform child in this.content)
        {
            GameObject.Destroy(child.gameObject);
        }

        SetContentSize();

        for (int i = 0; i < visibleHorizontalCount * verticalSlots; i++)
        {
            WordCardSetting prefab = Instantiate(this.wordCardPrefab, this.content);
            prefab.gameObject.SetActive(false);

            wordCardPool.Add(prefab);
        }

        PrintVisibleWordCards();
    }

    void SetContentSize()
    {
        int totalColumn = (this.swordRecordList.Count + verticalSlots - 1) / verticalSlots;
        float contentWidth = ((totalColumn * wordCardWidth) + ((totalColumn - 1) * horizontalSpacing));
        if(contentWidth < 0)
        {
            contentWidth = 0;
        }
        content.sizeDelta = new Vector2(contentWidth, content.sizeDelta.y); ;
    }

    void OnScrollMoving(Vector2 unused)
    {
        PrintVisibleWordCards();
    }

    void PrintVisibleWordCards()
    {
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

            if (dataIndex >= 0 && dataIndex < this.swordRecordList.Count)
            {
                visibleWordCard.gameObject.SetActive(true);

                int targetWordId = this.swordRecordList[dataIndex];
                visibleWordCard.SetData(
                    wordDataBase[targetWordId].kanji,
                    wordDataBase[targetWordId].meaning,
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
    }
}
