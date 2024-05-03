using UnityEngine;
using UnityEngine.UI;

public class Storage : MonoBehaviour
{
    const string CommandBottle = "Bottle";
    const string CommandJug = "Jug";
    const string CommandRock = "Rock";

    [SerializeField] private Text _text;    

    private string _idResource;
    private int _counterBottls;
    private int _counterJug;
    private int _counterRock;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Resource resource))
        {
            _idResource = resource.ID;
            CountResource();
            OutputValue();
        }
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
    
    private void OutputValue()
    {
        _text.text = $"Бутыли - {_counterBottls}; Кувшины - {_counterJug}; Камни - {_counterRock}";
    }
}