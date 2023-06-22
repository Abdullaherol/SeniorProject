using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour//Control player input and trigger UI 
{
    [SerializeField] private UIManager _uiManager;
    private ItemManager _itemManager;

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _maxDistance;

    private Camera _mainCamera;

    private void Start()//When scene started,this function triggered 
    {
        _itemManager = ItemManager.Instance;
        _mainCamera = Camera.main;
    }
    
    private void Update()//On each frame this function triggered
    {
        if (Input.GetMouseButtonDown(0))//If user click 
        {
            Fire();
        }
    }

    private void Fire() //Fire Raycast
    {
        if(IsMouseOverUI()) return;//Check Mouse over UI
        
        if(_uiManager.onUI) return;
        
        if (!Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, _maxDistance,
                _layerMask)) return;//With physics api,sending a ray and when ray hit item, take item component

        if (!_itemManager.GetItem(hit.transform.gameObject, out WorldItem item)) return;//Check item manager contains item
        
        _uiManager.ShowItemPanel(item);//Show item panel
    }

    private bool IsMouseOverUI()//Check mouse over UI
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}