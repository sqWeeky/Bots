using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBases : MonoBehaviour
{
    [SerializeField] private Base _prefab;

    private MoverBot _freeBot;
    private List<Base> _bases= new();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Base @base))
        {
            _bases.Add(@base);
        }
    }
}
