using UnityEngine;

public class Ball : MonoBehaviour {

    // config params
    [SerializeField] private float xPush = 10f;
    [SerializeField] private float yPush = 2f;
    [SerializeField] float randomFactor = 0.2f;
    [SerializeField] Paddle paddle1;
    [SerializeField] AudioClip[] ballSounds;
    

    // state
    private bool hasLaunched = false;
    Vector2 peddleToBallVector;

    // cached component references
    AudioSource audioSourceAsset;
    Rigidbody2D rigidBody2DAsset;

	// Use this for initialization
	void Start () {
        peddleToBallVector = transform.position - paddle1.transform.position;
        audioSourceAsset = GetComponent<AudioSource>();
        rigidBody2DAsset = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasLaunched == false)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
  
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rigidBody2DAsset.velocity = new Vector2(xPush, yPush);
            hasLaunched = true;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + peddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));

        if (hasLaunched)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            audioSourceAsset.PlayOneShot(clip);
            rigidBody2DAsset.velocity += velocityTweak;
        }
    }

}
