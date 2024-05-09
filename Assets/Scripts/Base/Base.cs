using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private List<MoverBot> _bots;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private float _delay;
    [SerializeField] private CreatorBot _creatorBot;
    [SerializeField] private Storage _storage;
    [SerializeField] private CreatorBase _creatorBase;
    [SerializeField] private Transform _spawn;

    private List<Resource> _resources;
    private int _index;
    private int _minValue = 0;
    private bool _isActiveBaseConstruction = false;

    public event Action BotSent;

    private void Awake()
    {
        _resources = new List<Resource>();
    }

    private void OnEnable()
    {
        _creatorBot.BotCreated += AddBot;
        _storage.FiveResourcesAccumulated += ActivateConstructionProcess;
    }

    private void Start()
    {
        StartCoroutine(ScanArea());
    }

    private void Update()
    {
        Work();
    }

    private void OnDisable()
    {
        _creatorBot.BotCreated -= AddBot;
        _storage.FiveResourcesAccumulated -= ActivateConstructionProcess;
    }

    public void GetBot(MoverBot bot)
    {
        bot.StopMover();
        bot.transform.position = _spawn.position;
        _bots.Add(bot);
    }

    private void Work()
    {
        if (_isActiveBaseConstruction == false)
        {
            foreach (MoverBot bot in _bots)
            {
                if (bot.IsFree == true && _resources.Count > 0)
                {
                    Carry(bot);
                }
            }
        }
        else if (_isActiveBaseConstruction == true)
        {
            foreach (MoverBot bot in _bots)
            {
                if (bot.IsFree == true)
                {
                    _isActiveBaseConstruction = false;
                    BuildNewBase(bot);
                    break;
                }
            }
        }
    }

    private IEnumerator ScanArea()
    {
        var waitForSeconds = new WaitForSeconds(_delay);

        while (true)
        {
            _resources.Clear();
            _resources = _scanner.TransferResources();
            yield return waitForSeconds;
        }
    }

    private void ActivateConstructionProcess()
    {
        _isActiveBaseConstruction = true;
    }

    private void BuildNewBase(MoverBot bot)
    {
        bot.BuildBase(_creatorBase.PositionNewBase);
        BotSent?.Invoke();
        _creatorBase.TakeBot(bot);
        _bots.Remove(bot);
    }

    private void AddBot()
    {
        _bots.Add(_creatorBot.TransferBot());
    }

    private void Carry(MoverBot bot)
    {
        GenerateRandomValue();
        bot.Operate(_resources[_index].transform, _resources[_index].ID, _storage.gameObject.transform);
        RemoveResource();
    }

    private void GenerateRandomValue() => _index = UnityEngine.Random.Range(_minValue, _resources.Count);

    private void RemoveResource() => _resources.RemoveAt(_index);
}