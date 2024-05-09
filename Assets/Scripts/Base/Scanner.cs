using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private List<Resource> _resources = new();
    private float _explosionRadius = 50f;    

    public List<Resource> TransferResources()
    {
        ScannResources();
        return _resources;
    }

    private void ScannResources()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider hit in hits)
            if (hit.gameObject.TryGetComponent(out Resource resource))
                if (resource.IsActive == true)
                    _resources.Add(resource);        
    }
}