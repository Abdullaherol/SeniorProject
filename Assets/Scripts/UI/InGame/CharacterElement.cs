using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterElement : MonoBehaviour
{
    public TMPro.TMP_InputField field;

    public WordSaveData wordSaveData;
    public CharacterSaveData characterSaveData;

    public void CheckIsHintOrCompleted()
    {
        if (characterSaveData.hint || characterSaveData.completed)
        {
            field.text = characterSaveData.character.ToString();
            field.readOnly = true;
        }
    }

    public void Hint()
    {
        field.text = characterSaveData.character.ToString();
        field.readOnly = true;

        var saveManager = SaveManager.Instance;
        saveManager.Hint(wordSaveData,characterSaveData);
    }

    public void OnValueChanged(string value)
    {
        var text = field.text;
        if (text != string.Empty)
        {
            if (field.text[0] == characterSaveData.character)
            {
                field.readOnly = true;
                
                var saveManager = SaveManager.Instance;
                saveManager.CompleteCharacter(wordSaveData,characterSaveData);
            }
            
            UIManager.Instance.NextField(this);
        }
    }

    public bool IsCompleted()
    {
        return characterSaveData.completed;
    }
}
