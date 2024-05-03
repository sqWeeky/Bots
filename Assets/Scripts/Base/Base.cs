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

    private void Awake()
    {
        _resources = new List<PointSpawn>();
    }

    private void Start()
    {
        _resources = _scanner.GetResources();
    }

    private void Update()
    {
        Work();
    }

    private void Work()
    {
        foreach (MoverBot bot in _bots)
        {
            if (bot.IsFree == true && _resources.Count > 0)
            {
                SendBot(bot);                
            }
        }
    }

    private void SendBot(MoverBot bot)
    {
        GenerateRandomValue();
        bot.Operate(_resources[_index].transform, _resources[_index].Resource.ID);
        RemoveResource();
    }

    private void GenerateRandomValue() => _index = Random.Range(_minValue, _resources.Count);

    private void RemoveResource() => _resources.RemoveAt(_index);
}