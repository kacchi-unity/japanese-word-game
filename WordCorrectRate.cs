using UnityEngine;

public enum WordCorrectDataType
{
    WordId,
    SuccessCount,
    FailCount,
}

[System.Serializable]
public class WordCorrectRate
{
    

    [SerializeField] private int wordId;
    [SerializeField] private int succesCount;
    [SerializeField] private int failCount;

    public float CorrectRate
    {
        get
        {
            int total = succesCount + failCount;
            if (total == 0)
            {
                return 0;
            }
            return ((float)succesCount/total)* 100f;
        }
    }

    public WordCorrectRate(int wordId)
    {
        this.wordId = wordId;
        this.succesCount = 0;
        this.failCount = 0;
    }

    public int WordId => wordId;
    public int SuccessCount => succesCount;
    public int FailCount => failCount;

    public int GetId()
    {
        return this.wordId;
    }

    public void IncreaseCount(bool isSucces)
    {
        if (isSucces) this.succesCount++;
        else this.failCount++;
    }

    public void ResetCount()
    {
        this.succesCount = 0;
        this.failCount = 0;
    }
}
