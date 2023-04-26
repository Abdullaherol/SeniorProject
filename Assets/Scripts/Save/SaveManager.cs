using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public int eachCharacterPoint;
    
    public List<WordSaveData> words;

    private void Awake()
    {
        Instance = this;

        GetSave();
    }

    public void Save()
    {
        var json = JsonConvert.SerializeObject(words);
        
        PlayerPrefs.SetString("Words",json);
    }

    private void GetSave()
    {
        if (PlayerPrefs.HasKey("Words"))
        {
            var json = PlayerPrefs.GetString("Words");

            words = JsonConvert.DeserializeObject<List<WordSaveData>>(json);
        }
        else
        {
            words = new List<WordSaveData>();
        }
    }

    public WordSaveData GetWordSaveData(string word)
    {
        if(words.Any(x => x.word == word))
        {
            return words.First(x => x.word == word);
        }
        
        var wordSaveData =  CreateWordSaveData(word);
        words.Add(wordSaveData);
        
        Save();

        return wordSaveData;
    }

    public int GetWordPoint(string word)
    {
        return words.First(x => x.word == word).CalculatePoint();
    }
    
    public void Hint(WordSaveData wordSaveData, CharacterSaveData characterSaveData)
    {
        words.First(x => x == wordSaveData).HintCharacter(characterSaveData);
        Save();
    }
    
    public void CompleteCharacter(WordSaveData wordSaveData, CharacterSaveData characterSaveData)
    {
        words.First(x=>x == wordSaveData).CompleteCharacter(characterSaveData);
        Save();
    }

    private WordSaveData CreateWordSaveData(string word)
    {
        return new WordSaveData(word);
    }

    public void SetCompleted(WordSaveData wordSaveData)
    {
        words.First(x=>x == wordSaveData).SetCompleted();
        Save();
    }
}