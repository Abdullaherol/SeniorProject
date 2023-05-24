using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterElement : MonoBehaviour//On word panel each character ui class
{
    public TMPro.TMP_InputField field;//writable character field

    public WordSaveData wordSaveData;//word data
    public CharacterSaveData characterSaveData;//character data

    public void CheckIsHintOrCompleted()//check hinted or completed
    {
        if (characterSaveData.hint || characterSaveData.completed)//if hinted or completed
        {
            field.text = characterSaveData.character.ToString();//Show correct character to user
            field.readOnly = true;//set readonly to field
        }
    }

    public void Hint()//User want to get hint this character
    {
        field.text = characterSaveData.character.ToString();//Show correct character to user
        field.readOnly = true;//Set readonly to fileld

        var saveManager = SaveManager.Instance;//get save manager instance
        saveManager.Hint(wordSaveData,characterSaveData);//save hinted 
    }

    public void OnValueChanged(string value)//On user enter character
    {
        var text = field.text;//get user input
        if (text != string.Empty)//check is empty
        {
            if (field.text[0] == characterSaveData.character)//get character of input of user
            {
                field.readOnly = true;//set readonly to field
                
                var saveManager = SaveManager.Instance;//get save manager instance
                saveManager.CompleteCharacter(wordSaveData,characterSaveData);//save completed 
            }
            
            UIManager.Instance.NextField(this);//set cursor position to next character
        }
    }

    public bool IsCompleted()//Return completed
    {
        return characterSaveData.completed;
    }
}
