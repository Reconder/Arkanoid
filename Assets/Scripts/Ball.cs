using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] PaddleMovement paddle;
    [SerializeField] float releaseVelocity = 10f;
    [SerializeField] float xreleaseVelocity = 2f;
    [SerializeField] float randomFactor;
    Vector2 distancePaddleToBall;
    Rigidbody2D myRigidbody;
    bool Locked;
    [SerializeField] AudioClip[] ballSounds;
    // Start is called before the first frame update
    AudioSource myAudioSource;

    void Start()
    {
        distancePaddleToBall = transform.position - paddle.transform.position;
        Locked = true;
        myAudioSource = GetComponent<AudioSource>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Locked) { Attached(); Release(); }

    }

    private void Attached() => transform.position = new Vector2(paddle.transform.position.x, paddle.transform.position.y + distancePaddleToBall.y);

    private void Release()
    {
        if (Input.GetMouseButtonUp(0))
        {
            myRigidbody.velocity = new Vector2(xreleaseVelocity, releaseVelocity);
            Locked = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!Locked) {
            myAudioSource.PlayOneShot(ballSounds[Random.Range(0, ballSounds.Length)]);
            
            myRigidbody.velocity += new Vector2(0f, Random.Range(-randomFactor, randomFactor)); //to make completely horizontal movement impossible
        }
    }
}
