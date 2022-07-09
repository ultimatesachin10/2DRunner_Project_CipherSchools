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
    //[SerializeField] private GameObject _pickablekey;
 
    private void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();

/*
         for (int i = 0; i < 21; i++)
         {
             Instantiate(_pickablekey, RandomPos(), Quaternion.identity);
         }
        
        if (_pickablekey.gameObject.CompareTag("Platform"))
        {
            _pickablekey.gameObject.SetActive(false);
        }
*/

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
        if (_count >= 210)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene(2);
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
    }
/*
    private Vector3 RandomPos()
    {
        int x, z;
        float y;

        x = Random.Range(-10, 123);
        z = 0;
        y = Random.Range(1, 12.25f);

        return new Vector3(x, y, z);

    }
*/
}
