using System;
using UnityEngine;

[RequireComponent(typeof(Click))]
public class CreatorBase : MonoBehaviour
{
    [SerializeField] private Torch _prefabTorch;
    [SerializeField] private Base _prefabeBase;

    private Ray ray;
    private RaycastHit hit;
    private Click _click;
    private bool _isActivated = false;
    private Torch _torch;
    private float _torchPositionY = 2f;
    private Vector3 _defaultTorchPosition = new(0, -5, 0);
    private Base _newBase;
    private MoverBot _bot;

    public Transform PositionNewBase => _torch.gameObject.transform;
    public event Action TorchPut;

    private void Awake()
    {
        _click = GetComponent<Click>();
        _torch = Instantiate(_prefabTorch, _defaultTorchPosition, Quaternion.identity);
        _torch.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _click.ActionCompleted += Activat;
        _torch.BotArrived += Creat;
    }

    void Update()
    {
        SetFlag();
    }
    private void OnDisable()
    {
        _click.ActionCompleted -= Activat;
        _torch.BotArrived -= Creat;
    }

    public void TakeBot(MoverBot bot)
    {
        _bot = bot;
    }

    private void SetFlag()
    {
        if (_isActivated == true)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                _torch.gameObject.transform.position = new Vector3(hit.point.x, _torchPositionY, hit.point.z);

                if (_torch != null)
                {
                    if (Input.GetKey(KeyCode.Mouse1) && hit.transform.TryGetComponent(out Ground ground))
                    {
                        _torch.transform.position = hit.point;
                        _torch.ActivateCollider();
                        ChangeParametrs();
                    }
                }
            }
        }
    }

    private void ChangeParametrs()
    {
        _isActivated = false;
        TorchPut?.Invoke();
    }

    private void Activat()
    {
        _isActivated = true;
        _torch.gameObject.SetActive(true);
    }

    private void Creat()
    {
        _newBase = Instantiate(_prefabeBase);
        _newBase.transform.position = _torch.gameObject.transform.position;
        _newBase.GetBot(_bot);
    }
}