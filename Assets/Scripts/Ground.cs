using UnityEngine;
public class Ground : MonoBehaviour
{
    private GameManager m_GameManager;
    private Vector3[] startpos = new Vector3[4];
    
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            startpos[i] = transform.GetChild(i).position;
        }
        m_GameManager = GameManager.Instance;
    }
    void Update()
    {
        switch (m_GameManager.gamestate)
        {
            case GameManager.GameState.Intro: Intro();
                break;
            case GameManager.GameState.Ingame: Ingame();
                break;
        }
    }

    private void Ingame()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).position -= transform.right * Time.deltaTime;
            if (i < 2)
            {
                if (transform.GetChild(i).position.x <= startpos[i].x - 7.17f)
                    transform.GetChild(i).position = startpos[i];
            }
            if (i >= 2)
            {
                var height = Random.Range(-0.9f, 2.6f);
                if (transform.GetChild(i).position.x <= -startpos[2].x)
                {
                    transform.GetChild(i).position = startpos[2] + new Vector3(0,height,0);
                }
            }
        }
    }
    void Intro()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).position = startpos[i];
        }
    }
}
