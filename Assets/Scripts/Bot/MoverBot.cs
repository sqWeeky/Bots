using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Taker), typeof(NavMeshAgent))]
public class MoverBot : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private List<Transform> _moveSpots = new();

    private NavMeshAgent _navMeshAgent;
    private Taker _taker;
    private bool _isFree = true;
    private float _minDistanse = 1.5f;
    private Coroutine _coroutine1;
    private int _spawnNumber = 0;
    //private int _storageNumber = 1;
    private int _targetNumber = 2;
    private int _spot = 0;
    private int _nextSpot;

    public bool IsFree => _isFree;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _taker = GetComponent<Taker>();
    }

    private void Start()
    {
        transform.position = _moveSpots[_spawnNumber].transform.position;
        _spot = _targetNumber;
    }

    public void Operate(Transform target, string id)
    {
        _moveSpots.Add(target);
        Activation();
    }

    private void Activation()
    {
        _isFree = false;
        _coroutine1 = StartCoroutine(Move());
    }

    private void Disactivation()
    {
        _isFree = true;
        //Debug.Log(_moveSpots.Count);
        _moveSpots.RemoveAt(_targetNumber);

        StopAllCoroutines();
    }

    private IEnumerator Move()
    {
        Debug.Log("Бот движится " + _isFree);
        if (_spot == _spawnNumber)
        {
            Disactivation();
        }

        while (Vector3.Distance(transform.position, _moveSpots[_spot].position) > _minDistanse)
        {
            transform.position = Vector3.MoveTowards(transform.position, _moveSpots[_spot].position, _speed * Time.deltaTime);
            yield return null;
        }


        _nextSpot = _spot - 1;
        _spot = _nextSpot % _moveSpots.Count;

        if (_spot < 0)
        {
            _spot = _spawnNumber;
        }

        StartCoroutine(Move());


        //Debug.LogError("Корутина движения");
        //transform.position = Vector3.MoveTowards(transform.position, target.position, _speed);
        //_taker.TakeResource(id);
        //transform.position = Vector3.MoveTowards(transform.position, _base.position, _speed * Time.deltaTime);
        ////_navMeshAgent.SetDestination(_base.position);
        //_taker.GiveResourse();
        //transform.position = Vector3.MoveTowards(transform.position, _spot.position, _speed * Time.deltaTime);
        //_isFree = true;


    }

    private void Run(Transform target)
    {

    }
}