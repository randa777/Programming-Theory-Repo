using UnityEngine;

// theory 1.Inheritance
public class Ball : Cube
{
    public float jumpPower;
    private bool isGround = false;
    private Rigidbody ballRb;

    void Awake()
    {
        ballRb = GetComponent<Rigidbody>();
    }

    // theory 2.polymorphism
    // cube is rotate but ball is bounding
    protected override void ObjectClicked()
    {
        if (isGround) {
            ballRb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isGround = false;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (!isGround)
        {
            isGround = true;
        }
    }
}
