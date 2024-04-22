using UnityEngine;
using UnityEngine.UI;

public class Storage : MonoBehaviour
{
    const string CommandBottle = "Bottle";
    const string CommandJug = "Jug";
    const string CommandRock = "Rock";

    [SerializeField] private Text _text;    

    private Resource _resource;
    private int _counterBottls;
    private int _counterJug;
    private int _counterRock;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Taker bot))
        {
            _resource = bot.GiveResourse();
            CountResource();
            OutputValue();
        }
    }

    private void CountResource()
    {
        switch (_resource.ID)
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
    
    private void OutputValue()
    {
        _text.text = $"Бутылей - {_counterBottls}; Кувшины - {_counterJug}; Камни - {_counterRock}";
    }
}