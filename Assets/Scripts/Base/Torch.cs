using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Torch : MonoBehaviour
{
    private BoxCollider _collider;

    public event Action BotArrived;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out MoverBot bot))
        {
            _collider.enabled = false;
            BotArrived?.Invoke();
            gameObject.SetActive(false);
        }
    }

    public void ActivateCollider()
    {
        _collider.enabled = true;
    }
}