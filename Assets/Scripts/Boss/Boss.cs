using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private BossWeakPoint _weakPoint;

    private void Update()
    {
        if (_weakPoint == null)
        {
            Debug.Log($"Boss detects WeakPoint is null");
        } 
    }
}
