using UnityEngine;

public static class PlayerMovementController
{
    public static float HorizontalInputController(float joystickHorizontal, float horizontalInput, Rigidbody rigidbody,
        float trust)
    {
#if UNITY_ANDROID
        horizontalInput = joystickHorizontal;
#else
        horizontalInput = Input.GetAxis("Horizontal");
#endif

        rigidbody.AddForce(Vector3.right * trust * horizontalInput, ForceMode.Acceleration);


        if (rigidbody.velocity.x < 0 && horizontalInput > 0)
        {
            rigidbody.AddForce(Vector3.right * trust * 0.05f * horizontalInput, ForceMode.Impulse);
        }

        if (rigidbody.velocity.x > 0 && horizontalInput < 0)
        {
            rigidbody.AddForce(Vector3.right * trust * 0.05f * horizontalInput, ForceMode.Impulse);
        }

        return horizontalInput;
    }

    public static float VerticalInputController(float joystickVertical, float verticalInput, Rigidbody rigidbody,
        float trust)
    {
        verticalInput = joystickVertical;
        rigidbody.AddForce(Vector3.forward * trust * verticalInput, ForceMode.Acceleration);


        if (rigidbody.velocity.z < 0 && verticalInput > 0)
        {
            rigidbody.AddForce(Vector3.forward * trust * 0.15f * verticalInput, ForceMode.Impulse);
        }

        if (rigidbody.velocity.z > 0 && verticalInput < 0)
        {
            rigidbody.AddForce(Vector3.forward * trust * 0.15f * verticalInput, ForceMode.Impulse);
        }

        return verticalInput;
    }

    public static void RotateOnMovement(Transform transform, float horizontalInput, float torque)
    {
        transform.eulerAngles = new Vector3(0, 0, -horizontalInput * torque);
    }

    public static void LimitSpeedOfMovement(Rigidbody rigidbody)
    {
        if (rigidbody.velocity.z > GameManager.Instance.maxPlayerVelocityY)
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y,
                GameManager.Instance.maxPlayerVelocityY);
        }
        else if (rigidbody.velocity.z < -GameManager.Instance.maxPlayerVelocityY * 1.3f)
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y,
                -GameManager.Instance.maxPlayerVelocityY * 2);
        }

        if (rigidbody.velocity.x > GameManager.Instance.maxPlayerVelocityY)
        {
            rigidbody.velocity = new Vector3(GameManager.Instance.maxPlayerVelocityX, rigidbody.velocity.y,
                rigidbody.velocity.z);
        }
        else if (rigidbody.velocity.x < -GameManager.Instance.maxPlayerVelocityY)
        {
            rigidbody.velocity = new Vector3(-GameManager.Instance.maxPlayerVelocityX, rigidbody.velocity.y,
                rigidbody.velocity.z);
        }
    }

    public static void StopOutOfBounds(Transform transform, Rigidbody rigidbody)
    {
        Vector3 worldToVievportposition = Camera.main.WorldToViewportPoint(transform.position);
        worldToVievportposition.x = Mathf.Clamp01(worldToVievportposition.x);

        if (worldToVievportposition.x > 0.99)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(-Vector3.right * GameManager.Instance.borderForce, ForceMode.Impulse);
        }

        if (worldToVievportposition.x < 0.01)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(Vector3.right * GameManager.Instance.borderForce, ForceMode.Impulse);
        }


        if (rigidbody.transform.position.z > GameManager.Instance.borderZ)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(-Vector3.forward * GameManager.Instance.borderForce, ForceMode.Impulse);
        }

        if (rigidbody.transform.position.z < -worldToVievportposition.z)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(Vector3.forward * GameManager.Instance.borderForce, ForceMode.Impulse);
        }
    }

    public static void BackOnShot(Rigidbody rigidbody, float shotForce)
    {
        if (rigidbody.velocity.z < 0)
        {
            rigidbody.AddForce(Vector3.back * shotForce/5, ForceMode.Impulse);
        }
        else
        {
            rigidbody.AddForce(Vector3.back * shotForce, ForceMode.Impulse);
        }
        
    }
}