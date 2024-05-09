using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Taker))]
public class MoverBot : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Transform _storage;
    private Transform _target;
    private Taker _taker;
    private bool _isFree = true;
    private float _minDistanse = 1.5f;
    private string _idResourse;
    private Coroutine _coroutine;

    public bool IsFree => _isFree;

    private void Awake()
    {
        _taker = GetComponent<Taker>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource))
        {
            if (resource.IsActive == true)
            {
                _taker.TakeResource(_idResourse);
            }
        }
    }

    public void Operate(Transform target, string id, Transform storage)
    {
        _isFree = false;
        _idResourse = id;
        _target = target;
        _storage = storage;
        Activation();
    }

    public void BuildBase(Transform torch)
    {
        _coroutine = StartCoroutine(CarryResourses(torch));
    }

    public void StopMover()
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator CarryResourses(Transform torch)
    {
        _isFree = true;

        yield return Run(torch);

        _isFree = true;

        yield break;
    }

    private void Activation()
    {
        StartCoroutine(Extract());
    }

    private void Disactivation()
    {
        _isFree = true;
    }

    private IEnumerator Extract()
    {
        yield return Run(_target);        

        yield return Run(_storage);

        _taker.GiveResourse();

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