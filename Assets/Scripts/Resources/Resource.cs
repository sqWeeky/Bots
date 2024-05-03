using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private string _id;

    private bool _isActive = false;

    public string ID => _id;
    public bool IsActive => _isActive;

    public void Activate()
    {
        _isActive = true; 
    }

    public void Deactivate()
    {
        _isActive = false;
    }
}