using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private NetworkManager _networkManager;
    
    public static ItemManager Instance;

    private Dictionary<GameObject, WorldItem> _items = new Dictionary<GameObject, WorldItem>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UIManager.Instance.OnWordCompleted += InstanceOnOnWordCompleted;
    }

    private void InstanceOnOnWordCompleted(WordSaveData wordsavedata)
    {
        CheckAllItemsCompleted(wordsavedata.word);
    }

    private void CheckAllItemsCompleted(string word)
    {
        bool completed = false;

        var saveManager = SaveManager.Instance;

        var words = _items.Select(x => x.Value.itemName);

        completed = words.All(x => saveManager.words.Select(y => y.word).Contains(x));

        if (!completed)
        {
            Debug.Log("bütün kelimeler kayıtlı değil ve tamamlanmadı");
            return;
        }

        completed = saveManager.words.All(x => x.completed);

        if (!completed)
        {
            Debug.Log("bütün kelimeler kayıtlı ama tamamlanmadı 2");
            return;
        }

        var sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        Debug.Log("next scene index: "+sceneIndex);

        if (SceneManager.sceneCountInBuildSettings > sceneIndex)
        {
            _networkManager.SaveProgress(sceneIndex, () =>
            {
            });
        }
    }

    public void AddItem(GameObject worldObject, WorldItem worldItem)
    {
        _items.Add(worldObject, worldItem);
    }

    public bool GetItem(GameObject worldObject, out WorldItem worldItem)
    {
        var result = _items.ContainsKey(worldObject);

        worldItem = null;

        if (result)
        {
            worldItem = _items[worldObject];
        }

        return result;
    }
}