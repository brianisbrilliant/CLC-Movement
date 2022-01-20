using UnityEngine;

public class MovingSphere : MonoBehaviour
{

    // add speed
    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 10f, maxAcceleration = 10f;
    
    [SerializeField, Range(0f, 1f)]
    float bounciness = 0.5f;

    [SerializeField]
    Rect allowedArea = new Rect(-5f, -5f, 10f, 10f);

    [SerializeField]
    Renderer dvdBackground;

    Vector3 velocity;

    void Update() {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        // playerInput.Normalize();    // all or nothing input, no chance for in-between values. Levelhead does this.
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);

        Vector3 desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

        // relative movement, relative to the sphere's previous location.
        Vector3 displacement = velocity * Time.deltaTime;

        Vector3 newPosition = transform.localPosition + displacement;

        // if(!allowedArea.Contains(new Vector2(newPosition.x, newPosition.z))) {        // Contains() knows if a position is within a Rect. Very cool.
        //     newPosition.x = Mathf.Clamp(newPosition.x, allowedArea.xMin, allowedArea.xMax);
        //     newPosition.z = Mathf.Clamp(newPosition.z, allowedArea.yMin, allowedArea.yMax);
        // }

        if(newPosition.x < allowedArea.xMin) {
            newPosition.x = allowedArea.xMin;
            velocity.x = -velocity.x * bounciness;
            ChangeColor();
        }
        else if(newPosition.x > allowedArea.xMax) {
            newPosition.x = allowedArea.xMax;
            velocity.x = -velocity.x * bounciness;
            ChangeColor();
        }
        if(newPosition.z < allowedArea.yMin) {
            newPosition.z = allowedArea.yMin;
            velocity.z = -velocity.z * bounciness;
            ChangeColor();
        }
        else if(newPosition.z > allowedArea.yMax) {
            newPosition.z = allowedArea.yMax;
            velocity.z = -velocity.z * bounciness;
            ChangeColor();
        }

        transform.localPosition = newPosition;
    }

    void ChangeColor() {
        dvdBackground.material.color = Random.ColorHSV();
    }
}
