using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private int _speed;
    [SerializeField] private LayerMask platformsLayerMask;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;
    public TextMeshProUGUI countScore;
    [SerializeField] private Text Timeleft;
    public int Duration;
    private int remaingDuration;
    private int _count;
    [SerializeField] private GameObject _gameoverplan;
    [SerializeField] private GameObject _portal;
    [SerializeField] private GameObject _comingsoon;
 
    private void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();

    }
    public void Start()
    {
        Timer(Duration);
        _count = 0;
        SetCountScore();
    }

    private void Update()
    {
       float HorizontalMove = Input.GetAxisRaw("Horizontal");
        float jump = Input.GetAxisRaw("Jump");

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            float jumpVelocity = 10f;
            rigidbody2d.velocity = Vector2.up * jumpVelocity;
        }
        if (jump > 0)
        {
            _animator.SetBool("canJump", true);
        }
        else
        {
            _animator.SetBool("canJump", false);
        }

        movePlayer(HorizontalMove);
        Vector3 localScale = transform.localScale;

            if (HorizontalMove > 0)
        {
            _animator.SetBool("isRunning", true);
            localScale.x = Mathf.Abs(localScale.x);
        }
        else if (HorizontalMove < 0)
        {
            _animator.SetBool("isRunning", true);
            localScale.x = -1f * Mathf.Abs(localScale.x);
        }
        else
        {
            _animator.SetBool("isRunning", false);
        }

        transform.localScale = localScale;


        if (remaingDuration <= 0)
        {
            _animator.SetBool("Death", true);
            _gameoverplan.SetActive(true);
        }
        else
        {
            _animator.SetBool("Death", false);
        }
    }

    private void movePlayer(float horizontal)
    {
        Vector3 position = transform.position;
        position.x += horizontal * _speed * Time.deltaTime;
        transform.position = position;
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
        return raycastHit2d.collider != null;
    }

    void SetCountScore()
    {
        countScore.text = "Score : " + _count.ToString();
        if (_count >= 160)
        {
            _portal.SetActive(true);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //SceneManager.LoadScene(2);
        }
        else
        {
            _portal.SetActive(false);
        }
    }

    private void Timer(int Second)
    {
        remaingDuration = Second;
        StartCoroutine(UpdateTimer());
    }
    private IEnumerator UpdateTimer()
    {
        while (remaingDuration >= 0)
        {
            Timeleft.text = $"{remaingDuration / 60:00} : {remaingDuration % 60:00}";
            remaingDuration--;
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickableKey"))
        {
            other.gameObject.SetActive(false);
            _count = _count + 10;
            SetCountScore();
        }
        if (other.gameObject.CompareTag("Portal"))
        {
            other.gameObject.SetActive(false);
            SceneManager.LoadScene(2);
        }
        if (other.gameObject.CompareTag("PickableRoyalKey"))
        {
            other.gameObject.SetActive(false);
            _count = _count + 10;
            SetCountScore();
        }
        if (other.gameObject.CompareTag("Portal2"))
        {
            other.gameObject.SetActive(false);
            //SceneManager.LoadScene(2);
            _comingsoon.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            _comingsoon.SetActive(false);
        }
    }
}
