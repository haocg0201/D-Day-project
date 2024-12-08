using UnityEngine;

public class Knife : Weapon
{
    public override void Start()
    {
        base.Start();
        Initialize("knife","Dao cùn",20,5,1,"Ta rất sắc đấy.",0.8f);
    }

    public override void Update(){
        base.Update();
    }
}
