using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor; //Dirty
#endif

[CreateAssetMenu(fileName = "SwordRecordSO", menuName = "ScriptableObject/SwordRecordSO")]
public class SwordRecordSO : ScriptableObject
{
    [Header("--- Backup data (Unity saves) ---")]
    [SerializeField] private List<int> swordRecordList = new List<int>();
    [SerializeField] private List<int> unusedList = new List<int>();
    [SerializeField] private List<WordCorrectRate> correctRateList = new List<WordCorrectRate>();
    [SerializeField] private List<int> lastChangedIds = new List<int>(); //to in game -> lobby backup

    [Header("--- Runtime data (Speed tools) ---")]
    private HashSet<int> swordRecordHash = new HashSet<int>();
    private HashSet<int> unusedHash = new HashSet<int>();
    private Dictionary<int, WordCorrectRate> correctRateDic = new Dictionary<int, WordCorrectRate>();

    
    private void ValidateDataSet()
    {
        if (unusedHash.Count == 0 && unusedList.Count > 0)
        {
            this.unusedHash = unusedList.ToHashSet();
        }

        if (swordRecordHash.Count == 0 && swordRecordList.Count > 0)
        {
            swordRecordHash = swordRecordList.ToHashSet();
        }

        if (correctRateDic.Count == 0 && correctRateList.Count > 0)
        {
            correctRateDic = correctRateList.ToDictionary(x => x.GetId(), x => x);
        }
    }

    private void SyncList()
    {
        this.unusedList = this.unusedHash.ToList();
        this.swordRecordList = this.swordRecordHash.ToList();
        this.correctRateList = this.correctRateDic.Values.ToList();

#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }

    public void InitializeDataset(int count)
    {
        ResetUnused();
        ResetCorrectRate();
        for (int i = 1; i <= count; i++)
        {
            this.unusedHash.Add(i);
            this.correctRateDic.Add(i, new WordCorrectRate(i));
        }
        ResetSwordRecord();

        SyncList();
    }

    public List<int> GetRandomId(int amount)
    {
        ValidateDataSet();

        List<int> unusedPool = this.unusedHash.ToList<int>();
        List<int> randomResult= new List<int>();

        this.lastChangedIds.Clear();

        if (unusedPool.Count < amount)
        {
            int tmp = amount - unusedPool.Count;
            List<int> swordRecordPool = this.swordRecordHash.ToList<int>();

            //select in Sword Record Pool
            for (int i = 1; i <= tmp && swordRecordPool.Count > 0; i++)
            {
                int randomIndex = Random.Range(0, swordRecordPool.Count);
                int selectedId = swordRecordPool[randomIndex];
                randomResult.Add(selectedId);
                swordRecordPool.RemoveAt(randomIndex);

                this.lastChangedIds.Add(selectedId);
            }

            //select in Unused List Pool
            foreach (int item in unusedPool)
            {
                randomResult.Add(item);
                this.swordRecordHash.Add(item);

                this.lastChangedIds.Add(item);
            }

            ResetUnused();
        }
        else
        {
            //select in Unused List Pool
            for (int i = 1; i <= amount && unusedPool.Count > 0; i++)
            {
                int randomIndex = Random.Range(0, unusedPool.Count);
                int selectedId = unusedPool[randomIndex];

                randomResult.Add(selectedId);
                this.swordRecordHash.Add(selectedId);
                this.unusedHash.Remove(selectedId);
                unusedPool.RemoveAt(randomIndex);

                this.lastChangedIds.Add(selectedId);
            }
        }

        SyncList();
        return randomResult;
    }

    public void Rollback()
    {
        ValidateDataSet();

        if (this.lastChangedIds.Count == 0) return;

        foreach (int item in this.lastChangedIds)
        {
            this.swordRecordHash.Remove(item);
            this.unusedHash.Add(item);

            if (this.correctRateDic.ContainsKey(item))
            {
                this.correctRateDic[item].ResetCount();
            }
        }
        Debug.Log($"데이터 복구완료, 사전에 {lastChangedIds.Count}개의 단어를 넣는 것을 취소합니다.");
        Debug.Log($"데이터 복구완료, 사전에 {lastChangedIds.Count}개 단어 정답률을 초기화합니다.");
        this.lastChangedIds.Clear();

        SyncList();
    }

    //to build test
    public void ShowSwordRecordList()
    {
        ValidateDataSet();

        if (this.swordRecordHash != null)
        {
            foreach (var item in this.swordRecordHash)
            {
                Debug.Log($"검심 id: {item}, 정답률: {this.GetCorrectRate(item)}");
            }
        }
        Debug.Log($"Sword Record해시 내 총 개수: {this.swordRecordHash.Count}");
        Debug.Log($"Unused 해시 내 총 개수: {this.unusedHash.Count}");
    }

    public void RecordCorrectResult(int wordId, bool isSuccess)
    {
        ValidateDataSet();
        if (!correctRateDic.ContainsKey(wordId))
        {
            correctRateDic.Add(wordId, new WordCorrectRate(wordId));
            Debug.Log($"예외처리: Id {wordId} 단어 정답률 리스트를 새로 만듭니다.");
        }

        correctRateDic[wordId].IncreaseCount(isSuccess);
        SyncList();
    }

    public float GetCorrectRate(int wordId)
    {
        ValidateDataSet();
        if (this.correctRateDic.ContainsKey(wordId))
        {
            return this.correctRateDic[wordId].correctRate;
        }
        else
        {
            Debug.LogWarning($"ID {wordId}의 정답률 데이터가 없어 새로 생성합니다.");
            WordCorrectRate newRateData = new WordCorrectRate(wordId);

            this.correctRateDic.Add(wordId, newRateData);

            SyncList();

            return this.correctRateDic[wordId].correctRate;
        }
    }

    public HashSet<int> GetSwordRecordHash()
    {
        ValidateDataSet();
        return this.swordRecordHash;
    }

    public HashSet<int> GetUnusedHash()
    {
        ValidateDataSet();
        return this.unusedHash;
    }

    public void ResetSwordRecord()
    {
        this.swordRecordHash.Clear();
        this.swordRecordList.Clear();
    }

    public void ResetUnused()
    {
        this.unusedHash.Clear();
        this.unusedList.Clear();
    }

    public void ResetCorrectRate()
    {
        this.correctRateDic.Clear();
        this.correctRateList.Clear();
    }
}
