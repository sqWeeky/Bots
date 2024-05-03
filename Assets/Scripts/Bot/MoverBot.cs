using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Taker))]
public class MoverBot : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _storage;
    [SerializeField] private Transform _spot;

    private Transform _target;
    private Taker _taker;
    private bool _isFree = true;
    private float _minDistanse = 1.5f;
    private string _idResourse;

    public bool IsFree => _isFree;

    private void Awake()
    {
        _taker = GetComponent<Taker>();
    }

    private void Start()
    {
        transform.position = _spot.position;
    }

    public void Operate(Transform target, string id)
    {
        _idResourse = id;
        _target = target;
        Activation();
    }

    private void Activation()
    {
        _isFree = false;
        StartCoroutine(Move());
    }

    private void Disactivation()
    {
        _isFree = true;
    }

    private IEnumerator Move()
    {
        yield return Run(_target);

        _taker.TakeResource(_idResourse);

        yield return Run(_storage);

        _taker.GiveResourse();

        yield return Run(_spot);

        Disactivation();
        yield break;
    }

    private IEnumerator Run(Transform target)
    {
        while (Vector3.Distance(transform.position, target.position) >= _minDistanse)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
            yield return null;
        }
    }
}