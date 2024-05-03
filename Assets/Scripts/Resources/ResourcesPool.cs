using System.Collections.Generic;
using UnityEngine;

public class ResourcesPool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Resource[] _prefabs;

    private int _index;
    private Resource _resource;
    private Queue<Resource> _pool;

    private void Awake()
    {
        _pool = new Queue<Resource>();
    }

    public Resource GenerateObgect(Transform spawn)
    {
        if (_pool.Count == 0)
        {
            _index = Random.Range(0, _prefabs.Length);
            var resource = Instantiate(_prefabs[_index]);
            resource.transform.parent = _container;
            resource.transform.position = spawn.position;

            return resource;
        }

        return _pool.Dequeue();
    }

    public Resource GetObgect(string id)
    {
        _resource = null;

        foreach (Resource resourse in _pool)
        {
            if (resourse.ID == id && resourse.IsActive == false)
            {
                resourse.Activate();
                resourse.gameObject.SetActive(true);
                _resource = resourse;
                break;
            }
        }

        return _resource;
    }

    public void PutObject(Resource resource)
    {
        resource.gameObject.SetActive(false);
        resource.Deactivate();
        _pool.Enqueue(resource);
    }
}