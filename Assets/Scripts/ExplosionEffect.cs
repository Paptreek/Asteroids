using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 2.0f);
    }
}
