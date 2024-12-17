using UnityEngine;

public class Akm : Weapon
{
    public override void Start()
    {
        base.Start();
        Initialize("akm","AKM",10,2,1.3f,"Hmmm ... thật vô vị.",0.3f);
    }

    public override void Update(){
        base.Update();
    }
}
