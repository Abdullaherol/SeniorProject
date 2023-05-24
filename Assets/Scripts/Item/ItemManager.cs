using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour//Control and manage all items
{
    [SerializeField] private NetworkManager _networkManager;
    
    public static ItemManager Instance;//Singleton

    private Dictionary<GameObject, WorldItem> _items = new Dictionary<GameObject, WorldItem>();//item dictionary

    private void Awake()//Awake Function for singleton
    {
        Instance = this;//assign singleton variable
    }

    private void Start()//start function
    {
        UIManager.Instance.OnWordCompleted += InstanceOnOnWordCompleted;//assign method to onwordcompleted event
    }

    private void InstanceOnOnWordCompleted(WordSaveData wordsavedata)
    {
        CheckAllItemsCompleted(wordsavedata.word);
    }

    private void CheckAllItemsCompleted(string word)//CHeck all items completed
    {
        bool completed = false;

        var saveManager = SaveManager.Instance;

        var words = _items.Select(x => x.Value.itemName);//get words from item list

        completed = words.All(x => saveManager.words.Select(y => y.word).Contains(x));//check all items are completed

        if (!completed)
        {
            Debug.Log("bütün kelimeler kayıtlı değil ve tamamlanmadı");
            return;
        }

        completed = saveManager.words.All(x => x.completed);//check all items are completed

        if (!completed)
        {
            Debug.Log("bütün kelimeler kayıtlı ama tamamlanmadı 2");
            return;
        }

        var sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;//if completed then get next scene index

        Debug.Log("next scene index: "+sceneIndex);

        if (SceneManager.sceneCountInBuildSettings > sceneIndex)//if next scene index in range of settings
        {
            _networkManager.SaveProgress(sceneIndex, () =>//save progress and send saveProgress request to rest API
            {
            });
        }
    }

    public void AddItem(GameObject worldObject, WorldItem worldItem)//add item to item dictionary 
    {
        _items.Add(worldObject, worldItem);//add item to item dictionary
    }

    public bool GetItem(GameObject worldObject, out WorldItem worldItem)//Get item from item dictionary
    {
        var result = _items.ContainsKey(worldObject);//Check item dictionary contains item

        worldItem = null;

        if (result)//if contains item in item dictionary
        {
            worldItem = _items[worldObject];//get item and return with out
        }

        return result;//return contains result
    }
}