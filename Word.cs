using static Unity.VisualScripting.Member;

[System.Serializable]

public class Word
{
    public int id;
    public string kanji;
    public string meaning;
    public string reading;
    public int jlpt;

    public Word(Word source)
    {
        this.id = source.id;
        this.kanji = source.kanji;
        this.meaning = source.meaning;
        this.reading = source.reading;
        this.jlpt = source.jlpt;
    }
}
