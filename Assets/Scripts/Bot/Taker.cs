using System.Collections.Generic;
using UnityEngine;

public class Taker : MonoBehaviour
{
    [SerializeField] private List<Resource> _prefabs;
    [SerializeField] private Transform _hands;

    private Resource _resource;
    private List<Resource> _resources = new();

    private void Awake()
    {
        foreach (var resource in _prefabs)
        {
            _resource = Instantiate(resource);
            _resource.transform.parent = _hands;
            _resources.Add(_resource);
            _resource.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (_resource != null)
            _resource.transform.position = _hands.position;
    }

    public void TakeResource(string id)
    {
        foreach (var resource in _resources)
        {
            if (resource.ID == id)
            {
                _resource = resource;
                _resource.gameObject.SetActive(true);
                break;
            }
        }

        _resource.transform.position = _hands.position;
    }

    public void GiveResourse()
    {
        if (_resource != null)
        {
            _resource.gameObject.SetActive(false);
            _resource = null;
        }
    }
}