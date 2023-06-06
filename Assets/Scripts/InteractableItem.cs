using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    [SerializeField]
    private int _highlightIntensity = 4;    
    private Outline _outline;
    private Collider _collider;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    public void SetFocus()
    {
        _outline.OutlineWidth = _highlightIntensity;
    }
    
    public void RemoveFocus()
    {
        _outline.OutlineWidth = 0;
    }
    public void PickUp(Transform position)
    {
        transform.SetParent(position);
        
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        
        _collider.isTrigger = true;
        _rigidbody.isKinematic = true;
    }

    public void Drop()
    {
        transform.SetParent(null,true);
        _collider.isTrigger = false;
          
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(Vector3.down);
    }

    public void ThrowAway(Vector3 direction, float power)
    {
        _collider.isTrigger = false;
        _rigidbody.isKinematic = false;
        transform.SetParent(null);
        _rigidbody.AddForce(direction * power);
    }
}