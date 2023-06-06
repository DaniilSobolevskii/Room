using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    
    private Camera _camera;
    
    [SerializeField]
    private Transform _hand;

    [SerializeField] private float _power;

    private InteractableItem _lastItem;
    private InteractableItem _usedItem;
   
    private void Start()
    {
        _camera = GetComponentInChildren<Camera>();
        
    }
    
    private void Update()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out var hitInfo)) // Кидаем луч 
        {
            if (hitInfo.collider.GetComponent<Door>()&& Input.GetKey(KeyCode.E))// если видим дверь и нажата кнопка Е - открываем
            {
                hitInfo.collider.GetComponent<Door>().SwitchDoorState();
            }
            
            if ( hitInfo.collider.GetComponent<InteractableItem>()) // Если видим  интерактивный обьект
            {
                var interactableItem = hitInfo.collider.gameObject.GetComponent<InteractableItem>();
                
                SetFocus(interactableItem); // подсвечиваем его
                
                if (Input.GetKey(KeyCode.E))
                {
                    TryGetItem(interactableItem);
                }
            }
            else
            {
                if (_lastItem != null)
                {
                    _lastItem.RemoveFocus();
                    _lastItem = null;
                }
            }
            
        }
        
        if (Input.GetMouseButtonDown(0) && _usedItem != null)
        {
            _usedItem.ThrowAway(transform.forward, _power);
            _usedItem = null;
        }
        
    }

    private void SetFocus(InteractableItem item)
    {
        
        if (_lastItem == null)
        {
            _lastItem = item;
            _lastItem.SetFocus();
        }
        else
        {
            _lastItem.RemoveFocus();
            _lastItem = item;
            _lastItem.SetFocus();
        }
    }

    private void TryGetItem(InteractableItem item)
    {
        if (_usedItem != item)
        {
            if (_usedItem!=null)
            {
                _usedItem.Drop();
                        
            }
            item.PickUp(_hand);
            _usedItem = item;
        }
        
        
    }
}
