using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class SaveManager : MonoBehaviour//Save manager
{
    public static SaveManager Instance;//Singleton

    public int eachCharacterPoint;//each character point
    
    public List<WordSaveData> words;// saved words

    private void Awake()//Awake Function for singleton
    {
        Instance = this;//assign singleton variable

        GetSave();//Get save from local
    }

    public void Save()//Save words data
    {
        var json = JsonConvert.SerializeObject(words);//Convert words data to json
        
        PlayerPrefs.SetString("Words",json);//save json data to local
    }

    private void GetSave()//Get save data from local
    {
        if (PlayerPrefs.HasKey("Words"))//Check local has words data
        {
            var json = PlayerPrefs.GetString("Words");//get json data from local

            words = JsonConvert.DeserializeObject<List<WordSaveData>>(json);//convert local json data to words data
        }
        else
        {
            words = new List<WordSaveData>();//if not has words data create new words list
        }
    }

    public WordSaveData GetWordSaveData(string word)//Get Word save data from word
    {
        if(words.Any(x => x.word == word))//Check contains words list 
        {
            return words.First(x => x.word == word);//return word save data
        }
        
        var wordSaveData =  CreateWordSaveData(word);//create word save data
        words.Add(wordSaveData);//add created data to words list
        
        Save();//save new word save data

        return wordSaveData;//return word save data
    }

    public int GetWordPoint(string word)//Get word point from word name
    {
        return words.First(x => x.word == word).CalculatePoint(); //Calculate word point and return
    }
    
    public void Hint(WordSaveData wordSaveData, CharacterSaveData characterSaveData)//Set hinted to word
    {
        words.First(x => x == wordSaveData).HintCharacter(characterSaveData);//find word and set hinted
        Save();//save words data
    }
    
    public void CompleteCharacter(WordSaveData wordSaveData, CharacterSaveData characterSaveData)//Set completed to word
    {
        words.First(x=>x == wordSaveData).CompleteCharacter(characterSaveData);//find word and set completed
        Save();//save words data
    }

    private WordSaveData CreateWordSaveData(string word)//Create word save data
    {
        return new WordSaveData(word);//create word save data and return
    }

    public void SetCompleted(WordSaveData wordSaveData)//Set Completed to word
    {
        words.First(x=>x == wordSaveData).SetCompleted();//find word and set completed
        Save();//Save words data
    }
}