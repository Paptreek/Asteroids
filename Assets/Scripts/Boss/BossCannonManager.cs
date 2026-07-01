using UnityEngine;

public class BossCannonManager : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _cannonParentTransform;
    [SerializeField] private Transform[] _cornerCannonTransforms = new Transform[4];
    [SerializeField] private Transform[] _centerCannonTransforms = new Transform[4];

    private float _cornerBulletTimer = 1.0f;
    private float _centerBulletTimer = 2.0f;
    private float _cannonParentAngle;
    private int[] _cornerCannonFireAngles = { 45, 135, 225, 315 };
    private int[] _centerCannonFireAngles = { 0, 90, 180, 270 };
    private BossCannon[] _centerCannons = new BossCannon[4];

    private void Start()
    {
        for (int i = 0; i < _centerCannons.Length; i++)
        {
            _centerCannons[i] = _centerCannonTransforms[i].GetComponent<BossCannon>();
        }
    }

    private void Update()
    {
        _cornerBulletTimer -= Time.deltaTime;
        _centerBulletTimer -= Time.deltaTime;

        _cannonParentAngle = _cannonParentTransform.localEulerAngles.z;

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
        for (int i = 0; i < _cornerCannonTransforms.Length; i++)
        {
            if (_cornerCannonTransforms[i] != null)
            {
                Bullet bullet = Instantiate(_bullet, _cornerCannonTransforms[i].position, Quaternion.identity);
                bullet.SetFiringShip(FiringShip.Enemy);
                bullet.SetFiringDirection(_cornerCannonFireAngles[i] + _cannonParentAngle);
                bullet.SetScreenWrappable(false);
            }
        }

        _cornerBulletTimer = 2.0f;
    }

    private void FireCenterCannons()
    {
        for (int i = 0; i < _centerCannonTransforms.Length; i++)
        {
            if (_centerCannonTransforms[i] != null && !_centerCannons[i].IsDamaged)
            {
                Bullet bullet = Instantiate(_bullet, _centerCannonTransforms[i].position, Quaternion.identity);
                bullet.SetFiringShip(FiringShip.Enemy);
                bullet.SetFiringDirection(_centerCannonFireAngles[i] + _cannonParentAngle);
                bullet.SetScreenWrappable(false);
            }
        }

        _centerBulletTimer = 2.0f;
    }
}
