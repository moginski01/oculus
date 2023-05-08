using System.IO;
using System.Net;
using UnityEngine;


// Definiujemy strukturę danych, którą chcemy zapisać w pliku JSON
[System.Serializable]
public class ModelData
{
    public string name;
    public int age;
	public float height;
}

public class MyJsonWriter
{
    // Ścieżka do pliku, w którym chcemy zapisać dane
    public string filePath = Application.persistentDataPath + "/mydata.json";
    public ModelData data;

    public MyJsonWriter(ModelData data)
    {
        this.data = data;
    }
    public void SaveData()
    {
        // Serializujemy dane do formatu JSON
        string json = JsonUtility.ToJson(this.data);

        Debug.Log(filePath);

        // Zapisujemy dane do pliku
        File.WriteAllText(filePath, json);
        Debug.Log("koniec zapisu");
    }

    public void LoadData()
    {
        string userData = File.ReadAllText(filePath);
        this.data = JsonUtility.FromJson<ModelData>(userData);
        Debug.Log("Wczytano z pliku");
    }
}

