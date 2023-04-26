using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class WordSaveData
{
    public string word;
    public List<CharacterSaveData> characters;
    public bool completed;

    public WordSaveData()
    {
        
    }

    public WordSaveData(string word, List<CharacterSaveData> characters)
    {
        this.word = word;
        this.characters = characters;
    }
    public WordSaveData(string word)
    {
        var characterPoint = SaveManager.Instance.eachCharacterPoint;
        
        this.word = word;
        characters = new List<CharacterSaveData>();

        for (int i = 0; i < word.Length; i++)
        {
            CharacterSaveData characterSaveData = new CharacterSaveData(word[i]);
            
            characters.Add(characterSaveData);
        }
    }

    public bool CheckCompleted()
    {
        bool allCompleted = characters.All(character => character.completed);

        return allCompleted;
    }

    public void SetCompleted()
    {
        completed = true;
    }


    public int CalculatePoint()
    {
        var characterPoint = SaveManager.Instance.eachCharacterPoint;
        
        var totalPoint = characters.Sum(character => character.GetPoint() * characterPoint);
        
        return totalPoint;
    }

    public int HintCharacter(char character)
    {
        characters.First(x=>x.character == character).Hint();

        return CalculatePoint();
    }
    
    public int HintCharacter(CharacterSaveData characterSaveData)
    {
        characters.First(x=>x == characterSaveData).Hint();

        return CalculatePoint();
    }

    public void CompleteCharacter(char character)
    {
        characters.First(x=>x.character == character).Completed();
    }
    public void CompleteCharacter(CharacterSaveData characterSaveData)
    {
        characters.First(x=>x == characterSaveData).Completed();
    }
}