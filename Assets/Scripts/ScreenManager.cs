using UnityEngine;

public class ScreenManager
{
    public static void WrapAroundScreen(Transform transform, float maximumX, float maximumY)
    {
        if (transform.position.x <= -maximumX)
        {
            transform.position = new Vector3(-transform.position.x - 0.25f, transform.position.y);
        }
        else if (transform.position.x >= maximumX)
        {
            transform.position = new Vector3(-transform.position.x + 0.25f, transform.position.y);
        }
        else if (transform.position.y <= -maximumY)
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y - 0.25f);
        }
        else if (transform.position.y >= maximumY)
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y + 0.25f);
        }
    }
}
