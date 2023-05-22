using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    private ItemManager _itemManager;

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _maxDistance;

    private Camera _mainCamera;

    private bool _onItemPanel;

    private void Start()
    {
        _itemManager = ItemManager.Instance;
        _mainCamera = Camera.main;
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    private void Fire() //Fire Raycast
    {
        if(IsMouseOverUI()) return;
        
        if (!Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, _maxDistance,
                _layerMask)) return;

        if (!_itemManager.GetItem(hit.transform.gameObject, out WorldItem item)) return;
        
        _uiManager.ShowItemPanel(item);
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}