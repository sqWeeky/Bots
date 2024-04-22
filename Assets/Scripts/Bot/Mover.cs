using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform _storage;
    [SerializeField] private float _speed;

    private int _spot = 0;
    private int _nextSpot;
    private float _minDistanse = 0.2f;
    private List<Transform> _moveSpots = new(2);

    private void Awake()
    {
        
    }

    private IEnumerator Move()
    {

        while (Vector3.Distance(transform.position, _moveSpots[_spot].position) > _minDistanse)
        {
            transform.position = Vector3.MoveTowards(transform.position, _moveSpots[_spot].position, _speed * Time.deltaTime);
            yield return null;
        }

        _nextSpot = _spot + 1;
        _spot = _nextSpot % _moveSpots.Count;

        yield return null;
        StartCoroutine(Move());
    }
}
