using UnityEngine;

public class Taker : MonoBehaviour
{
    [SerializeField] private ResourcesPool _pool;
    [SerializeField] private Transform _hands;

    private Resource _resource;

    private void Update()
    {
        if (_resource != null)
            _resource.transform.position = _hands.position;
    }

    public void TakeResource(string id)
    {
        _resource = _pool.GetObgect(id);
        _resource.transform.position = _hands.position;
    }

    public void GiveResourse()
    {
        _pool.PutObject(_resource);
        _resource = null;
    }
}