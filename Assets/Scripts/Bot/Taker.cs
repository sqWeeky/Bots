using UnityEngine;

public class Taker : MonoBehaviour
{
    [SerializeField] private ResourcesPool _pool;
    [SerializeField] private Transform _hands;

    private Resource _resource;

    public void TakeResource(string id)
    {
        _resource = _pool.GetObgect(id, _hands);
    }

    public Resource GiveResourse()
    {
        _pool.PutObject(_resource);
        return _resource;
    }
}