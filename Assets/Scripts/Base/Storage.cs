using System;
using UnityEngine;

public class Storage : MonoBehaviour
{
    const string CommandBottle = "Bottle";
    const string CommandJug = "Jug";
    const string CommandRock = "Rock";

    [SerializeField] private CreatorBot _creatorBot;
    [SerializeField] private CreatorBase _creatorBase;
    [SerializeField] private Base _base;

    private string _idResource;
    private int _counterBottls;
    private int _counterJug;
    private int _counterRock;
    private int _numberRockForBase = 3;
    private int _numberBottleForBase = 1;
    private int _numberJugForBase = 1;
    private int _numberJugForBot = 1;
    private int _numberBottleForBot = 1;
    private int _numberRockForBot = 1;

    public event Action ThreeResourcesAccumulated;
    public event Action FiveResourcesAccumulated;

    private void OnEnable()
    {
        _creatorBot.BotCreated += SubtractResourcesForBot;
        _creatorBase.TorchPut += AccumulateResources;
        _base.BotSent += SubtractResourcesForBase;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Resource resource))
        {
            _idResource = resource.ID;
            CountResource();
        }
    }

    private void Update()
    {
        CheckAmountOfResourcesForBot();
        CheckAmountOfResourcesForBase();
    }

    private void OnDisable()
    {
        _creatorBot.BotCreated -= SubtractResourcesForBot;
        _creatorBase.TorchPut -= AccumulateResources;
        _base.BotSent -= SubtractResourcesForBase;
    }

    private void CountResource()
    {
        switch (_idResource)
        {
            case CommandBottle:
                _counterBottls++;
                break;
            case CommandJug:
                _counterJug++;
                break;
            case CommandRock:
                _counterRock++;
                break;
        }
    }

    private void AccumulateResources()
    {
        _creatorBot.enabled = false;
    }

    private void CheckAmountOfResourcesForBot()
    {
        if (_counterBottls >= _numberBottleForBot && _counterJug >= _numberJugForBot && _counterRock >= _numberRockForBot)
        {
            ThreeResourcesAccumulated?.Invoke();
        }
    }

    private void CheckAmountOfResourcesForBase()
    {
        if (_counterBottls >= _numberBottleForBase && _counterJug >= _numberJugForBase && _counterRock >= _numberRockForBase)
        {
            FiveResourcesAccumulated?.Invoke();
        }
    }

    private void SubtractResourcesForBase()
    {
        _counterBottls -= _numberBottleForBase;
        _counterJug -= _numberJugForBase;
        _counterRock -= _numberRockForBase;
        _creatorBot.enabled = true;
    }

    private void SubtractResourcesForBot()
    {
        _counterBottls -= _numberBottleForBot;
        _counterJug -= _numberJugForBot;
        _counterRock -= _numberRockForBot;
    }
}