using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D ball;
    [SerializeField] private GameObject startPoint;
    [SerializeField] private Rigidbody2D player;
    private bool _ballInGame = false;
    private bool _stillAlive = true;
    private int _lives = 3;
    [SerializeField] private Text textLives;
    [SerializeField] private Text gameOver;
    [SerializeField] private GameObject powerUpRed;
    [SerializeField] private GameObject powerUpBlue;
    [SerializeField] private GameObject blocks;
    private int qtdBlocks;

    // Start is called before the first frame update
    void Start()
    {
        ball.GetComponent<Rigidbody2D>();
        textLives.text = _lives.ToString() + (_lives > 1 ? " Lives " : " Life ");
        gameOver.text = "";
        qtdBlocks = blocks.transform.childCount;
        Debug.Log($"Block: {blocks.name} has {qtdBlocks} childs");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_ballInGame && _stillAlive)
        {
            ball.velocity = new Vector2(5, 5);
            _ballInGame = true;
        }

        if (!_stillAlive && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Respawn"))
        {
            Vector2 start = startPoint.transform.position;
            ball.velocity = Vector2.zero;
            ball.position = start;
            player.GetComponent<Joint2D>().enabled = true;
            _ballInGame = false;
            UseLife();
        }
    }

    private void UseLife()
    {
        _lives--;
        textLives.text = _lives.ToString() + (_lives > 1 ? " Lives " : " Life ");
        if (_lives == 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        int random = Random.Range(0, 10);
        if (other.gameObject.CompareTag("Blocks"))
        {
            qtdBlocks = blocks.transform.childCount;
            Debug.Log($"How many child has left {qtdBlocks}");
            Destroy(other.gameObject);
            if (random <= 2.0)
            {
                Instantiate(powerUpBlue, other.transform.position, Quaternion.identity);
            }

            if (random >= 8.0)
            {
                Instantiate(powerUpRed, other.transform.position, Quaternion.identity);
            }

            if (qtdBlocks - 1 == 0)
            {
                Vector2 start = startPoint.transform.position;
                ball.velocity = Vector2.zero;
                ball.position = start;
                player.GetComponent<Joint2D>().enabled = true;
                _ballInGame = false;
                Win();
            }
        }

        if (other.collider.CompareTag("Player"))
        {
            BallHitPaddle();
        }
    }

    private void Die()
    {
        _stillAlive = false;
        gameOver.text = "GAME OVER\n Press Space to Start Over";
    }

    private void Win()
    {
        _stillAlive = false;
        gameOver.text = "YOU WIN\n Press Space to Start Over";
    }

    private void BallHitPaddle()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 ballPosition = gameObject.transform.position;

        Vector2 delta = ballPosition - playerPosition;
        Vector2 direction = delta.normalized;
        
        gameObject.GetComponent<Rigidbody2D>().velocity = direction * 10.0f;
        Debug.Log($"player Position: {playerPosition}\nball Position: {ballPosition}\nDelta: {delta}\nDirection: {direction}\nball Velocity: {gameObject.GetComponent<Rigidbody2D>().velocity}");
    }
}
