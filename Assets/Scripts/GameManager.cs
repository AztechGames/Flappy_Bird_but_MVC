using UnityEngine;
public class GameManager : MonoBehaviour
{
    public Model Model;
    public View view;
    public Controller Controller;
    private void Start()
    {
        Model = new Model();
        Controller = new Controller(Model, view);
        Controller.Start();
    }
    void Update()
    {
      Controller.Synch();
    }
}
