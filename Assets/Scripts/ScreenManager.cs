using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static void WrapAroundScreen(Transform transform)
    {
        if (transform.position.x <= -17.5f)
        {
            transform.position = new Vector2(-transform.position.x, transform.position.y);
        }
        else if (transform.position.x >= 17.5f)
        {
            transform.position = new Vector2(-transform.position.x, transform.position.y);
        }
        else if (transform.position.y <= -13.0f)
        {
            transform.position = new Vector2(transform.position.x, -transform.position.y);
        }
        else if (transform.position.y >= 13.0f)
        {
            transform.position = new Vector2(transform.position.x, -transform.position.y);
        }
    }
}
