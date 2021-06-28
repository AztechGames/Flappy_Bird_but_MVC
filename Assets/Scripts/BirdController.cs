using UnityEngine;
public class BirdController : MonoBehaviour
{
   private Rigidbody2D m_Rigidbody2Dg;
   [SerializeField] private float jumppower;
   [SerializeField] private float downrotate;
   private GameManager m_GameManager;
   private Vector3 m_Startpos;
   private Quaternion m_Startrot;
   private Transform m_Transform;
   private void Start()
   {
      m_Transform = transform;
      m_Rigidbody2Dg = GetComponent<Rigidbody2D>();
      m_GameManager = GameManager.Instance;
      m_Startpos = transform.position;
      m_Startrot = m_Transform.rotation;
   }
   private void Update()
   { 
      switch (m_GameManager.gamestate)
      {
         case GameManager.GameState.Intro: Intro();
            break;
         case GameManager.GameState.Ingame: Ingame();
            break;
      }
   }
   void Intro()
   {
      transform.position = m_Startpos;
      m_Transform.rotation = m_Startrot;
      m_Transform.position = m_Transform.up * Mathf.PingPong(Time.time, .5f);
      m_Rigidbody2Dg.gravityScale = 0;
   }
   void Ingame()
   {
      m_Rigidbody2Dg.gravityScale = 1;
      if (Input.GetMouseButtonDown(0))
      {
         m_Rigidbody2Dg.velocity = Vector2.up * jumppower;
         m_Transform.eulerAngles = transform.forward * 50f;
      }
      else
      {
         m_Transform.eulerAngles -= transform.forward * downrotate;
      }
   }
   private void OnCollisionEnter2D(Collision2D other)
   {
      m_GameManager.gamestate = GameManager.GameState.Gameover;
   }
   private void OnTriggerExit2D(Collider2D other)
   {
      m_GameManager.score++;
      m_GameManager.CalculateScore();
   }
}
