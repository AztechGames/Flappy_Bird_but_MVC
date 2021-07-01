using UnityEngine;
public class Controller
{
    public View view;
    public Model model;

    public Controller(Model model, View view)
    {
        this.model = model;
        this.view = view;
    }
    public void Synch()
    {
        model.Score = PlayerPrefs.GetInt("score");
        view.CalculateScore(model.Score,model.Numbers);
        if (view.gstate == View.Gstate.Ingame)
        {
            view.PlayGround(model.Ground.transform,model.Groundstartpos);
            view.MovePlayer(model.Jumppower,model.Speed,model.Downrotate);
        }
        else if (view.gstate == View.Gstate.Intro)
        {
            view.StayGround(model.Ground.transform,model.Groundstartpos);
            view.StayInput(model.Startpos,model.Startrot);
        }
        else if (view.gstate == View.Gstate.Gameover)
        {
            view.StartGround(model.Ground.transform,model.Groundstartpos);
            view.Gameover();
            PlayerPrefs.SetInt("score",0);
        }
    }
    public void Start()
    {
        model.Numbers = Resources.LoadAll<Sprite>("Numbers");
        model.Startpos = view.transform.position;
        model.Startrot = view.transform.rotation;
    }
}
