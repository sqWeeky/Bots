using System;
using UnityEngine;

public class Click : MonoBehaviour
{
    public event Action ActionCompleted;

    private void OnMouseDown()
    {
        ActionCompleted?.Invoke();
    }
}