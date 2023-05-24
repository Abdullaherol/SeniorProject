using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class WordSaveData//Each word save data class
{
    public string word;//word
    public List<CharacterSaveData> characters;//characters of word
    public bool completed;//completed

    public WordSaveData()//Constructor
    {
        
    }

    public WordSaveData(string word, List<CharacterSaveData> characters)//Constructor
    {
        this.word = word;
        this.characters = characters;
    }
    public WordSaveData(string word)//Constructor
    {
        this.word = word;
        characters = new List<CharacterSaveData>();

        for (int i = 0; i < word.Length; i++)//Create each character of word
        {
            CharacterSaveData characterSaveData = new CharacterSaveData(word[i]);
            
            characters.Add(characterSaveData);
        }
    }

    public bool CheckCompleted()//Check completed
    {
        bool allCompleted = characters.All(character => character.completed);//check all characters completed

        return allCompleted;//return result
    }

    public void SetCompleted()//Set completed
    {
        completed = true;
    }


    public int CalculatePoint()//Calculate word point
    {
        var characterPoint = SaveManager.Instance.eachCharacterPoint;//Get save manager instance
        
        var totalPoint = characters.Sum(character => character.GetPoint() * characterPoint);//calculate point of word
        
        return totalPoint;//return result
    }

    public int HintCharacter(char character)//Set hinted character of word 
    {
        characters.First(x=>x.character == character).Hint();//find character and set hinted

        return CalculatePoint();//return point
    }
    
    public int HintCharacter(CharacterSaveData characterSaveData)//Set hinted character of word 
    {
        characters.First(x=>x == characterSaveData).Hint();//find character and set hinted

        return CalculatePoint();//return point
    }

    public void CompleteCharacter(char character)//Set completed
    {
        characters.First(x=>x.character == character).Completed();//find character and set completed
    }
    public void CompleteCharacter(CharacterSaveData characterSaveData)//Set completed
    {
        characters.First(x=>x == characterSaveData).Completed();//find character and set completed
    }
}