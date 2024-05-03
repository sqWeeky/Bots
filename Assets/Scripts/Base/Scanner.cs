using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private List<PointSpawn> _resources = new List<PointSpawn>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PointSpawn resource))
            _resources.Add(resource);
    }

    public List<PointSpawn> GetResources()
    {
        return _resources;
    }
}