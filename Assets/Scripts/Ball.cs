using UnityEngine;

public class Ball : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }

    public float speed = 1000f;
    private Vector2 initialVelocity;

    private void Awake(){
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start(){
        ResetBall();

    }

    public void ResetBall(){
        this.transform.position = Vector2.zero;
        this.rigidbody.velocity = Vector2.zero;
        Invoke(nameof(SetRandomTrajectory), 1f);
    }

    private void SetRandomTrajectory(){
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1.0f, 1.0f);
        force.y = -1f;

        this.rigidbody.AddForce(force.normalized * this.speed);
    }

}
