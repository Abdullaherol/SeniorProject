using System;
using UnityEngine;

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
        
        _uiManager.OnItemPanelOpened += UiManagerOnOnItemPanelOpened;
        _uiManager.OnItemPanelClosed += UiManagerOnOnItemPanelClosed;
    }

    private void UiManagerOnOnItemPanelClosed()
    {
        // throw new NotImplementedException();
    }

    private void UiManagerOnOnItemPanelOpened()
    {
        // throw new NotImplementedException();
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
        if (!Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, _maxDistance,
                _layerMask)) return;

        if (!_itemManager.GetItem(hit.transform.gameObject, out WorldItem item)) return;
        
        _uiManager.ShowItemPanel(item);
    }
}