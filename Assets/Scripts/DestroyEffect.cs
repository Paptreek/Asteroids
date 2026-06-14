using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 2.0f);
    }
}
