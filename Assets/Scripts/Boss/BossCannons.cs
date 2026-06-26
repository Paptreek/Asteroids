using UnityEngine;

public class BossCannons : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform[] _cornerCannons = new Transform[4];
    [SerializeField] private Transform[] _centerCannons = new Transform[4];

    private float _cornerBulletTimer = 1.0f;
    private float _centerBulletTimer = 1.0f;

    private void Update()
    {
        _cornerBulletTimer -= Time.deltaTime;
        _centerBulletTimer -= Time.deltaTime;

        if (_cornerBulletTimer <= 0)
        {
            FireCornerCannons();
        }

        if (_centerBulletTimer <= 0)
        {
            FireCenterCannons();
        }
    }

    private void FireCornerCannons()
    {
        for (int i = 0; i < _cornerCannons.Length; i++)
        {
            Bullet bullet = Instantiate(_bullet, _cornerCannons[i].position, Quaternion.identity);
            bullet.SetFiringShip(FiringShip.Enemy);
            bullet.SetFiringDirection(_cornerCannons[i].eulerAngles.z);
            bullet.SetScreenWrappable(false);
        }

        _cornerBulletTimer = 1.0f;
    }

    private void FireCenterCannons()
    {
        for (int i = 0; i < _centerCannons.Length; i++)
        {
            Bullet bullet = Instantiate(_bullet, _centerCannons[i].position, Quaternion.identity);
            bullet.SetFiringShip(FiringShip.Enemy);
            bullet.SetFiringDirection(_centerCannons[i].eulerAngles.z);
            bullet.SetScreenWrappable(false);
        }

        _centerBulletTimer = 1.0f;
    }
}
