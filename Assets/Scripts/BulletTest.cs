using UnityEngine;

public class BulletTest
{
    private PlayerAttack _firingShip;
    private float _firingShipSpeed;
    private float _baseSpeed;

    public float FiredSpeed { get; }

    public BulletTest(PlayerAttack firingShip)
    {
        _firingShip = firingShip;
        _firingShipSpeed = firingShip.GetComponent<Rigidbody2D>().linearVelocityY;

        FiredSpeed = _baseSpeed + _firingShipSpeed;
    }
}
