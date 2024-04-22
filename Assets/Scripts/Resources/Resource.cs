using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private string _id;

    public string ID => _id;
}