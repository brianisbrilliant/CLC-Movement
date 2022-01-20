using UnityEngine;

public class MovingSphere1 : MonoBehaviour
{

    // add speed
    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 10f, maxAcceleration = 10f;

    [SerializeField]
    Renderer dvdBackground;

    [SerializeField, Range(0f, 10f)]
    float jumpHeight = 2f;

    Vector3 velocity, desiredVelocity;

    Rigidbody body;

    bool desiredJump;
    bool onGround;

    void Awake () {
        body = GetComponent<Rigidbody>();
    }

    void Update() {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);

        desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;

        // if true, keep being true;
        desiredJump |= Input.GetButtonDown("Jump");     // |= the OR assignment operator. Cool!
    }

    void FixedUpdate() {
        velocity = body.velocity;
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
        
        if(desiredJump) {
            desiredJump = false;
            Jump();
        }

        body.velocity = velocity;

        onGround = false;       // do this at the end of FixedUpdate();

    }

    void Jump() {
        if(onGround) velocity.y += Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
    }

    void OnCollisionEnter() {
        onGround = true;
    }

    void OnCollisionStay() {
        onGround = true;
    }

    void ChangeColor() {
        if(dvdBackground != null) dvdBackground.material.color = Random.ColorHSV();
    }
}
