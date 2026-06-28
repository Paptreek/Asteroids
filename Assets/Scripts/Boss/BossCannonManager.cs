using UnityEngine;

public class BossCannonManager : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform[] _cornerCannons = new Transform[4];
    [SerializeField] private Transform[] _centerCannons = new Transform[4];

    private float _cornerBulletTimer = 1.0f;
    private float _centerBulletTimer = 2.0f;
    private int[] _cornerCannonFireAngles = { 45, 135, 225, 315 };
    private int[] _centerCannonFireAngles = { 0, 90, 180, 270 };

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

    private void SetCannonFiringAngles()
    {
        _cornerCannonFireAngles[0] = 45;
    }

    private void FireCornerCannons()
    {
        for (int i = 0; i < _cornerCannons.Length; i++)
        {
            if (_cornerCannons[i] != null)
            {
                Bullet bullet = Instantiate(_bullet, _cornerCannons[i].position, Quaternion.identity);
                bullet.SetFiringShip(FiringShip.Enemy);
                bullet.SetFiringDirection(_cornerCannonFireAngles[i]);
                bullet.SetScreenWrappable(false);
            }
        }

        _cornerBulletTimer = 2.0f;
    }

    private void FireCenterCannons()
    {
        for (int i = 0; i < _centerCannons.Length; i++)
        {
            if (_centerCannons[i] != null)
            {
                Bullet bullet = Instantiate(_bullet, _centerCannons[i].position, Quaternion.identity);
                bullet.SetFiringShip(FiringShip.Enemy);
                bullet.SetFiringDirection(_centerCannonFireAngles[i]);
                bullet.SetScreenWrappable(false);
            }
        }

        _centerBulletTimer = 2.0f;
    }
}
