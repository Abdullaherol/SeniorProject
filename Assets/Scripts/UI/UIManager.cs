using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject _panel;
    [SerializeField] private TMPro.TextMeshProUGUI _scoreText;
    
    [SerializeField] private Transform _charactersPanel;
    [SerializeField] private GameObject _characterPrefab;
    
    private ItemManager _itemManager;

    private WorldItem _selectedItem;

    public delegate void OnItemPanelOpenedHandler();
    public delegate void OnItemPanelClosedHandler();
    public delegate void OnWordCompletedHandler(WordSaveData wordSaveData);
    
    public event OnItemPanelOpenedHandler OnItemPanelOpened;
    public event OnItemPanelClosedHandler OnItemPanelClosed;
    public event OnWordCompletedHandler OnWordCompleted;

    private List<CharacterElement> elements = new List<CharacterElement>();

    private int _hint;

    private bool _onUI;

    private void Start()
    {
        Instance = this;
        
        _itemManager = ItemManager.Instance;
    }

    public void ShowItemPanel(WorldItem worldItem)
    {
        _onUI = true;
        
        _hint = 0;
        
        for (int i = 0; i < _charactersPanel.childCount; i++)
        {
            var child = _charactersPanel.GetChild(i);
            Destroy(child.gameObject);
        }
        
        elements.Clear();
        
        _panel.SetActive(true);
        
        _selectedItem = worldItem;

        OnItemPanelOpened?.Invoke();

        var saveManager = SaveManager.Instance;

        var wordSaveData = saveManager.GetWordSaveData(_selectedItem.itemName);
        
        for (int i = 0; i < _selectedItem.itemName.Length; i++)
        {
            var child = Instantiate(_characterPrefab, _charactersPanel);
            var element = child.GetComponent<CharacterElement>();

            var characterSaveData = wordSaveData.characters[i];
            
            element.wordSaveData = wordSaveData;
            element.characterSaveData = characterSaveData;

            element.CheckIsHintOrCompleted();
            
            if (i == 0 && !element.field.readOnly)
            {
                element.field.Select();
            }
            
            elements.Add(element);
        }

        ShowPoint();
    }

    private void ShowPoint()
    {
        var saveManager = SaveManager.Instance;
        
        _scoreText.text = "Score: "+saveManager.GetWordPoint(_selectedItem.itemName);
    }

    public void ShowHint()
    {
        _hint++;
        
        var emptyCharacters = elements.Where(x => x.field.text == string.Empty).ToList();

        if(emptyCharacters.Count == 0) return;
        
        var randomSelectedCharacter = emptyCharacters[Random.Range(0, emptyCharacters.Count)];
        randomSelectedCharacter.Hint();
        
        var saveManager = SaveManager.Instance;

        saveManager.Hint(randomSelectedCharacter.wordSaveData, randomSelectedCharacter.characterSaveData);

        ShowPoint();
    }

    public void HideItemPanel()
    {
        _onUI = false;
        
        _panel.SetActive(false);
        
        _selectedItem = null;
        
        OnItemPanelClosed?.Invoke();
        
        for (int i = 0; i < _charactersPanel.childCount; i++)
        {
            var child = _charactersPanel.GetChild(i);
            Destroy(child.gameObject);
        }
        
        elements.Clear();
    }

    public void NextField(CharacterElement element)
    {
        CheckWordCompleted();
        
        var index = elements.IndexOf(element)+1;

        if(elements.Count <= index) return;
        
        var nextElement = elements[index];

        if (nextElement.field.readOnly)
        {
            NextField(nextElement);
            return;
        }
            
        nextElement.field.Select();
        
        ShowPoint();
    }

    private void Update()
    {
        if(_onUI == false) return;

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            PreviousField();
        }
    }

    private void PreviousField()
    {
        CheckWordCompleted();
        
        var index = elements.IndexOf(elements.First(x => x.field.isFocused))-1;
        
        if(index < 0) return;
        
        var previousElement = elements[index];

        if (previousElement.field.readOnly)
        {
            PreviousField(previousElement);
            return;
        }
        
        previousElement.field.Select();
    }

    private void PreviousField(CharacterElement element)
    {
        var index = elements.IndexOf(element)-1;
        
        if(index < 0) return;
        
        var previousElement = elements[index];

        if (previousElement.field.readOnly)
        {
            PreviousField(previousElement);
            return;
        }
        
        previousElement.field.Select();
    }

    private void CheckWordCompleted()
    {
        var saveManager = SaveManager.Instance;

        var wordSaveData = saveManager.GetWordSaveData(_selectedItem.itemName);

        bool completed = wordSaveData.CheckCompleted();

        if (completed && !wordSaveData.completed)//for first completed time
        {
            Debug.Log("completed");
            //Finish Word and send point to smart contract
            saveManager.SetCompleted(wordSaveData);

            OnWordCompleted?.Invoke(wordSaveData);
        }
    }
}