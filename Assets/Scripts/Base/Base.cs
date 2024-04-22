using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private MoverBot[] _bots;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private int _delay;

    private List<PointSpawn> _resources;
    private int _index;
    private int _minValue = 0;
    //private int _maxValue = 0;
    private Coroutine _coroutine;

    private void Awake()
    {
        _resources = new List<PointSpawn>();
    }

    private void Start()
    {
        //_coroutine = StartCoroutine(Work());
        _resources = _scanner.GetResources();
    }

    private void Update()
    {
        Work();
    }

    private void Work()
    {
        //var wait = new WaitForSeconds(_delay);
        //yield return wait;

        foreach (MoverBot bot in _bots)
        {
            if (bot.IsFree == true && _resources.Count > 0)
            {
                Debug.LogError(bot.IsFree);
                SendBot(bot);
                
            }
        }

        //if (_resources.Count <= 0)
        //{
        //    StopCoroutine(_coroutine);
        //}

        //StartCoroutine(Work());
    }

    private void SendBot(MoverBot bot)
    {
        Debug.Log(bot.IsFree);
        Debug.LogError("Бот активирован");
        GenerateRandomValue();
        //Debug.Log(_index);
        //Debug.LogError(_resources.Count);
        bot.Operate(_resources[_index].transform, _resources[_index].Resource.ID);
        RemoveResource();
    }

    private void GenerateRandomValue() => _index = Random.Range(_minValue, _resources.Count);

    private void RemoveResource() => _resources.RemoveAt(_index);
}