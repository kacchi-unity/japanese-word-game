using System.IO;
using UnityEngine;

public interface ISaveSO
{
    void Save(string path);
    void Load(string path);
    string Name { get; }

}

public abstract class BaseSaveSO<T> : ScriptableObject, ISaveSO where T : new()
{
    public abstract void Initialize();

    [HideInInspector] public T runtimeData = new T();

    private string SaveFileName => $"{this.name}.json";

    public string Name => this.name;

    public void Save(string folderPath)
    {
        string fullPath = Path.Combine(folderPath, SaveFileName);
        string json = JsonUtility.ToJson(this.runtimeData, true);
        File.WriteAllText(fullPath, json);
        
    }

    public void Load(string folderPath)
    {
        string fullPath = Path.Combine(folderPath, SaveFileName);
        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            runtimeData = JsonUtility.FromJson<T>(json);
        }
        else
        {
            Initialize();
            Save(folderPath);
        }
    }
}
