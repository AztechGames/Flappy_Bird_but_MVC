using UnityEngine;
public class Model
{
    public float Jumppower = 4;
    public float Downrotate = 0.7f;
    public float Speed = 50f;
    public Vector3 Startpos;
    public Quaternion Startrot;
    public GameObject Ground = GameObject.Find("Ground");
    public Vector3[] Groundstartpos = new Vector3[4];
    public int Score = PlayerPrefs.GetInt("score",0);
    public Sprite[] Numbers;
}
