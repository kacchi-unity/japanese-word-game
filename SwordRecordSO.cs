using System.Collections.Generic;
using System.Linq;
using UnityEngine;


#if UNITY_EDITOR
using UnityEditor; //Dirty
#endif

[CreateAssetMenu(fileName = "SwordRecordSO", menuName = "ScriptableObject/SwordRecordSO")]
public class SwordRecordSO : BaseSaveSO<SwordRecordSO.RuntimeData>
{
    [Header("--- Runtime data (Speed tools) ---")]
    private HashSet<int> swordRecordHash = new HashSet<int>();
    private HashSet<int> unusedHash = new HashSet<int>();
    private Dictionary<int, WordCorrectRate> correctRateDic = new Dictionary<int, WordCorrectRate>();

    [System.Serializable]
    public class RuntimeData
    {
        public List<int> swordRecordList = new List<int>();
        public List<int> unusedList = new List<int>();
        public List<WordCorrectRate> correctRateList = new List<WordCorrectRate>();
        public List<int> lastChangedIds = new List<int>(); //to in game -> lobby backup
    }

    public override void Initialize()
    {
        if (WordLoader.Instance != null && WordLoader.Instance.runtimeData.wordDataBaseList != null)
        {
            int count = WordLoader.Instance.runtimeData.wordDataBaseList.Count;

            if (count > 0)
            {
                this.InitializeDataset(count);
                Debug.Log($"{this.name}: {count}개의 단어로 성공적으로 초기화되었습니다.");
            }
            else
            {
                Debug.LogError($"{this.name}: 의 단어 리스트가 비어있습니다.");
            }
        }

        else
        {
            Debug.LogError("단어 데이터 소스 리스트 요소 개수를 찾을 수 없습니다.");
        }
    }

    private void ValidateDataSet()
    {
        if (unusedHash.Count == 0 && runtimeData.unusedList.Count > 0)
        {
            this.unusedHash = runtimeData.unusedList.ToHashSet();
        }

        if (swordRecordHash.Count == 0 && runtimeData.swordRecordList.Count > 0)
        {
            swordRecordHash = runtimeData.swordRecordList.ToHashSet();
        }

        if (correctRateDic.Count == 0 && runtimeData.correctRateList.Count > 0)
        {
            correctRateDic = new Dictionary<int, WordCorrectRate>();

            foreach (var x in runtimeData.correctRateList)
            {
                int id = x.GetId();
                if (!correctRateDic.ContainsKey(id))
                {
                    correctRateDic.Add(id, x);
                }
            }
        }
    }

    private void SyncList()
    {
        runtimeData.unusedList = this.unusedHash.ToList();
        runtimeData.swordRecordList = this.swordRecordHash.ToList();
        runtimeData.correctRateList = this.correctRateDic.Values.ToList();

#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }

    void InitializeDataset(int count)
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

        runtimeData.lastChangedIds.Clear();

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

                runtimeData.lastChangedIds.Add(selectedId);
            }

            //select in Unused List Pool
            foreach (int item in unusedPool)
            {
                randomResult.Add(item);
                this.swordRecordHash.Add(item);

                runtimeData.lastChangedIds.Add(item);
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

                runtimeData.lastChangedIds.Add(selectedId);
            }
        }

        SyncList();
        return randomResult;
    }

    public void Rollback()
    {
        ValidateDataSet();

        if (runtimeData.lastChangedIds.Count == 0) return;

        foreach (int item in runtimeData.lastChangedIds)
        {
            this.swordRecordHash.Remove(item);
            this.unusedHash.Add(item);

            if (this.correctRateDic.ContainsKey(item))
            {
                this.correctRateDic[item].ResetCount();
            }
        }
        Debug.Log($"데이터 복구완료, 사전에 {runtimeData.lastChangedIds.Count}개의 단어를 넣는 것을 취소합니다.");
        Debug.Log($"데이터 복구완료, 사전에 {runtimeData.lastChangedIds.Count}개 단어 정답률을 초기화합니다.");
        runtimeData.lastChangedIds.Clear();

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
            Debug.LogWarning($"ID {wordId}의 정답률 데이터가 없어 새로 생성합니다: 초기 정답률 = 0% ");
            WordCorrectRate newRateData = new WordCorrectRate(wordId);

            this.correctRateDic.Add(wordId, newRateData);

            SyncList();

            return this.correctRateDic[wordId].correctRate;
        }
    }

    public List<int> GetSwordRecordList()
    {
        return runtimeData.swordRecordList;
    }

    public void ResetSwordRecord()
    {
        this.swordRecordHash.Clear();
        runtimeData.swordRecordList.Clear();
    }

    public void ResetUnused()
    {
        this.unusedHash.Clear();
        runtimeData.unusedList.Clear();
    }

    public void ResetCorrectRate()
    {
        this.correctRateDic.Clear();
        runtimeData.correctRateList.Clear();
    }
}
