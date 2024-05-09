using System;
using UnityEngine;
using UnityEngine.UI;

public class CreatorBot : MonoBehaviour
{
    [SerializeField] private Storage _storage;
    [SerializeField] private MoverBot _botPrefab;
    [SerializeField] private Transform _spawn;
    [SerializeField] private Transform _botsContainer;
    [SerializeField] private Text _massage;

    private MoverBot _bot;

    public event Action BotCreated;

    private void OnEnable()
    {
        _storage.ThreeResourcesAccumulated += CreatBot;
    }

    private void OnDisable()
    {
        _storage.ThreeResourcesAccumulated -= CreatBot;
    }

    public MoverBot TransferBot()
    {
        return _bot;
    }

    private void CreatBot()
    {
        _bot = Instantiate(_botPrefab);
        BotCreated?.Invoke();
        _bot.transform.parent = _botsContainer;
        _bot.transform.position = _spawn.transform.position;
    }
}