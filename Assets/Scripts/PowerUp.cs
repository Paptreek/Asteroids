using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private AbilityManager _abilityManager;

    private void Update()
    {
        Destroy(gameObject, 5.0f);
    }

    public void SetAbilityManager(AbilityManager abilityManager)
    {
        _abilityManager = abilityManager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _abilityManager.HasMultiShot = true;
            Destroy(gameObject);
        }
    }
}
