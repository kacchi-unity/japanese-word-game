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

    [Header("--- Runtime data (Speed tools) ---")]
    private HashSet<int> swordRecordHash = new HashSet<int>();
    private HashSet<int> unusedHash = new HashSet<int>();

    [SerializeField] private List<int> lastChangedIds = new List<int>(); //to in game -> lobby backup

    private void ValidateHash()
    {
        if (unusedHash.Count == 0 && unusedList.Count > 0)
        {
            this.unusedHash = new HashSet<int>(unusedList);
        }
        if (swordRecordHash.Count == 0 && swordRecordList.Count > 0)
        {
            swordRecordHash = new HashSet<int>(swordRecordList);
        }
    }

    private void SyncList()
    {
        this.unusedList = this.unusedHash.ToList();
        this.swordRecordList = this.swordRecordHash.ToList();

#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }

    public void SetUnusedHash(int count)
    {
        ResetUnusedHash();
        for (int i = 1; i <= count; i++)
        {
            this.unusedHash.Add(i);
        }
        ResetSwordRecordHash();

        SyncList();
    }

    public List<int> GetRandomId(int amount)
    {
        ValidateHash();

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

            ResetUnusedHash();
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
        ValidateHash();

        if (this.lastChangedIds.Count == 0) return;

        foreach (int item in this.lastChangedIds)
        {
            this.swordRecordHash.Remove(item);
            this.unusedHash.Add(item);
        }
        Debug.Log($"데이터 복구완료, 사전에 {lastChangedIds.Count}개의 단어를 넣는 것을 취소합니다.");
        this.lastChangedIds.Clear();

        SyncList();
    }

    public void ShowSwordRecordList()
    {
        ValidateHash();

        if (this.swordRecordHash != null)
        {
            foreach (var item in this.swordRecordHash)
            {
                Debug.Log($"검심 일단 id만 호출: {item}");
            }
        }
        Debug.Log($"Sword Record해시 내 총 개수: {this.swordRecordHash.Count}");
        Debug.Log($"Unused 해시 내 총 개수: {this.unusedHash.Count}");
    }

    public HashSet<int> GetSwordRecordHash()
    {
        ValidateHash();
        return this.swordRecordHash;
    }

    public HashSet<int> GetUnusedHash()
    {
        ValidateHash();
        return this.unusedHash;
    }

    public void ResetSwordRecordHash()
    {
        this.swordRecordHash.Clear();
        this.swordRecordList.Clear();
    }

    public void ResetUnusedHash()
    {
        this.unusedHash.Clear();
        this.unusedList.Clear();
    }
}
