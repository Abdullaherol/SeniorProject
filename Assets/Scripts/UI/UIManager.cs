using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class UIManager : MonoBehaviour//Control user input and show word panel
{
    public static UIManager Instance;//Singleton

    [SerializeField] private GameObject _panel;//main word panel
    [SerializeField] private TMPro.TextMeshProUGUI _scoreText;//word score text
    
    [SerializeField] private Transform _charactersPanel;//characters panel
    [SerializeField] private GameObject _characterPrefab;//each character prefab
    
    private WorldItem _selectedItem;

    public delegate void OnWordCompletedHandler(WordSaveData wordSaveData);//On Word Completed Handler
    
    public event OnWordCompletedHandler OnWordCompleted;//On Word Completed Event

    private List<CharacterElement> elements = new List<CharacterElement>();//ui charcter elements list

    private bool _onUI;//User on UI

    private void Awake()//Awake Function
    {
        Instance = this;//assign singleton variable
    }

    public void ShowItemPanel(WorldItem worldItem)//Show Word Item Panel
    {
        _onUI = true;//set on uı

        for (int i = 0; i < _charactersPanel.childCount; i++)//Clear previous word character elements
        {
            var child = _charactersPanel.GetChild(i);
            Destroy(child.gameObject);
        }
        
        elements.Clear();//clear element list
        
        _panel.SetActive(true);//set visible element panel
        
        _selectedItem = worldItem;//set last selected item variable to selected item

        var saveManager = SaveManager.Instance;//get save manager instance

        var wordSaveData = saveManager.GetWordSaveData(_selectedItem.itemName);//get word save data of selected item
        
        for (int i = 0; i < _selectedItem.itemName.Length; i++)//Create selected item character elements
        {
            var child = Instantiate(_characterPrefab, _charactersPanel);//create elmenet
            var element = child.GetComponent<CharacterElement>();//get characterelement comnponenet

            var characterSaveData = wordSaveData.characters[i];//get character data
            
            element.wordSaveData = wordSaveData;//set word element data to word data
            element.characterSaveData = characterSaveData;//set character element data to character data

            element.CheckIsHintOrCompleted();//check is hinted or completed
            
            if (i == 0 && !element.field.readOnly)
            {
                element.field.Select();//if is first character select
            }
            
            elements.Add(element);//add element to elements list
        }

        ShowPoint();//show word point
    }

    private void ShowPoint()//Show Word Point
    {
        var saveManager = SaveManager.Instance;//get save manager instance
        
        _scoreText.text = "Score: "+saveManager.GetWordPoint(_selectedItem.itemName);//update score text
    }

    public void ShowHint()//Show hint
    {
        var emptyCharacters = elements.Where(x => x.field.text == string.Empty).ToList();//get empty character

        if(emptyCharacters.Count == 0) return;//if are there any empty character
        
        var randomSelectedCharacter = emptyCharacters[Random.Range(0, emptyCharacters.Count)];//get random empty character
        randomSelectedCharacter.Hint();//set hinted
        
        var saveManager = SaveManager.Instance;//get save manager instance

        saveManager.Hint(randomSelectedCharacter.wordSaveData, randomSelectedCharacter.characterSaveData);//save hinted character

        ShowPoint();//show new point
    }

    public void HideItemPanel()//Hide word item panel
    {
        _onUI = false;//set onUI false
        
        _panel.SetActive(false);//set panel invisible
        
        _selectedItem = null;//set selected item null
        
        for (int i = 0; i < _charactersPanel.childCount; i++)//destroy created elements from panel
        {
            var child = _charactersPanel.GetChild(i);
            Destroy(child.gameObject);
        }
        
        elements.Clear();//clear elements list
    }

    public void NextField(CharacterElement element)//Go next character field on word panel
    {
        CheckWordCompleted();//Check word completed
        
        var index = elements.IndexOf(element)+1;//get index of element

        if(elements.Count <= index) return;
        
        var nextElement = elements[index];

        if (nextElement.field.readOnly)
        {
            NextField(nextElement);//go next element
            return;
        }
            
        nextElement.field.Select();//select next element
        
        ShowPoint();//show new point
    }

    private void Update()//Update Function
    {
        if(_onUI == false) return;//if onUI is false return

        if (Input.GetKeyDown(KeyCode.Backspace))//if user key backspace
        {
            PreviousField();//go previous field
        }
    }

    private void PreviousField()//Go previous character field
    {
        CheckWordCompleted();//Check word completed
        
        var index = elements.IndexOf(elements.First(x => x.field.isFocused))-1;//get index of element
        
        if(index < 0) return;
        
        var previousElement = elements[index];

        if (previousElement.field.readOnly)
        {
            PreviousField(previousElement);//Go previous element field
            return;
        }
        
        previousElement.field.Select();//select previous element field
    }

    private void PreviousField(CharacterElement element)//Go previous character field
    {
        var index = elements.IndexOf(element)-1;//get index of element
        
        if(index < 0) return;
        
        var previousElement = elements[index];

        if (previousElement.field.readOnly)
        {
            PreviousField(previousElement);//Go previous element field
            return;
        }
        
        previousElement.field.Select();//select previous element field
    }

    private void CheckWordCompleted()//Check word completed
    {
        var saveManager = SaveManager.Instance;//get save manager instance

        var wordSaveData = saveManager.GetWordSaveData(_selectedItem.itemName);//get word save data from save manager

        bool completed = wordSaveData.CheckCompleted();//check completed

        if (completed && !wordSaveData.completed)//for first completed time
        {
            Debug.Log("completed");
            //Finish Word and send point to smart contract
            saveManager.SetCompleted(wordSaveData);

            OnWordCompleted?.Invoke(wordSaveData);
        }
    }
}