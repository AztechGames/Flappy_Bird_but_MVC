using UnityEngine;
public class View : MonoBehaviour
{
    public GameObject gameOver,taptoStart;
    [SerializeField] private SpriteRenderer Units, Tens, Hundreds;
    public enum Gstate
    {
        Intro,
        Ingame,
        Gameover
    }
    public Gstate gstate;
    private void Start()
    {
        gstate = Gstate.Intro;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && gstate == Gstate.Intro)
            gstate = Gstate.Ingame;
    }
    public void MovePlayer(float jumppower,float speed,float downrotate)
    {
        gameOver.SetActive(false);
        taptoStart.SetActive(false);
        var rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = 1;
        if (Input.GetMouseButtonDown(0))
        {
            rigidbody2D.velocity = Vector2.up * jumppower;
            transform.eulerAngles = transform.forward * speed;
        }
        else
        {
            transform.eulerAngles -= transform.forward * downrotate;
        }
        
    }
    public void StayInput(Vector3 startpos,Quaternion startrot)
    {
        taptoStart.SetActive(true);
        gameOver.SetActive(false);
        var rigidbody2D = transform.GetComponent<Rigidbody2D>();
        transform.position = startpos;
        transform.rotation = startrot;
        transform.position = transform.up * Mathf.PingPong(Time.time, .5f);
        rigidbody2D.gravityScale = 0;
    }
    public void StayGround(Transform ground,Vector3[] groundstart)
    {
        for (int i = 0; i < ground.childCount ; i++)
        {
            groundstart[i] = ground.GetChild(i).position;
        }
    }
    public void PlayGround(Transform ground,Vector3[] groundstart)
    {
        for (int i = 0; i < ground.childCount; i++)
        {
            ground.GetChild(i).position -= ground.right * Time.deltaTime;
            if (i < 2)
            {
                if (ground.GetChild(i).position.x <= groundstart[i].x - 7.17f)
                    ground.GetChild(i).position = groundstart[i];
            }
            if (i >= 2)
            {
                var height = UnityEngine.Random.Range(-0.9f, 2.6f);
                if (ground.GetChild(i).position.x <= -groundstart[2].x)
                {
                    ground.GetChild(i).position = groundstart[2] + new Vector3(0,height,0);
                }
            }
        }
    }
    public void StartGround(Transform ground,Vector3[] groundstart)
    {
        for (int i = 0; i < ground.childCount; i++)
        {
            ground.GetChild(i).position = groundstart[i];
        }
    }
    public void Gameover()
    {
        gameOver.SetActive(true);
        taptoStart.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        gstate = Gstate.Gameover;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        var _score = PlayerPrefs.GetInt("score");
        _score++;
        PlayerPrefs.SetInt("score",_score);
    }
    public void CalculateScore(int score,Sprite[]Numbers)
    {
        if (score < 10)
            Units.sprite = Numbers[score];
        else if(score >=10 && score < 100)
        {
            Units.transform.parent.position = new Vector3(-.25f,0,0);
            Tens.enabled = true;
            Tens.sprite = Numbers[score / 10];
            Units.sprite = Numbers[score % 10];
        }
        else if(score >= 100)
        {
            Units.transform.parent.position = Vector3.zero;
            Hundreds.enabled = true;
            Hundreds.sprite = Numbers[score / 100];
            int rest = score % 100;
            Tens.sprite = Numbers[rest / 10];
            Units.sprite = Numbers[rest % 10];
        }
    }
    public void RestartGame()
    {
        gstate = Gstate.Intro;
    }
}
