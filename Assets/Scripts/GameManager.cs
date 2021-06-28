using UnityEngine;
public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _ınstance;
    public static GameManager Instance {get => _ınstance;}
    private void Awake()
    {
        if (_ınstance == null)
            _ınstance = this;
    }
    #endregion
    public enum GameState
    {
        Intro,
        Ingame,
        Gameover
    }
    public GameState gamestate;
    [SerializeField] private Sprite[] Numbers;
    [SerializeField] private SpriteRenderer Units, Tens, Hundreds;
    [SerializeField] private GameObject GameOver, TaptoStart;
    public int score;
    private void Update()
    {
        switch (gamestate)
        {
            case GameState.Intro: Intro();
                break;
            case GameState.Ingame: Ingame();
                break;
            case GameState.Gameover: Gameover();
                break;
        }
    }
    void Intro()
    {
        score = 0;
        Units.sprite = Numbers[score];
        Units.transform.parent.position = new Vector3(-.5f, 0, 0);
        Tens.enabled = false;
        Hundreds.enabled = false;
        TaptoStart.SetActive(true);
        GameOver.SetActive(false);
    }
    void Ingame()
    {
        GameOver.SetActive(false);
        TaptoStart.SetActive(false);
    }
    void Gameover()
    {
        GameOver.SetActive(true);
        TaptoStart.SetActive(false);
    }
    public void CalculateScore()
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
    public void StartGame()
    {
        gamestate = GameState.Ingame;
    }
    public void RestartGame()
    {
        gamestate = GameState.Intro;
    }
}
