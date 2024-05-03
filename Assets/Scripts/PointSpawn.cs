using UnityEngine;

public class PointSpawn : MonoBehaviour
{
    [SerializeField] private ResourcesPool _pool;

    private bool _isOccupied;
    private Resource _resource;

    public bool IsOccupied => _isOccupied;
    public Resource Resource => _resource;

    private void Awake()
    {
        _isOccupied = false;
    }

    private void Start()
    {
        GenerationResource();
        _isOccupied = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out MoverBot bot))
        {
            _isOccupied = false;
            _resource.gameObject.SetActive(false);
            _pool.PutObject(_resource);
        }
    }

    private void GenerationResource()
    {
        _resource = _pool.GenerateObgect(transform);
        _resource.gameObject.SetActive(true);
    }
}