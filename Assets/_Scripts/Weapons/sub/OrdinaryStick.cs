using UnityEngine;

public class OrdinaryStick : Weapon
{
    public override void Start()
    {
        base.Start();
        Initialize("ordinaryStick","Cây gậy tầm thường",15,5,1,"Xào xạc, xào xạc.",1f);
    }

    public override void Update(){
        base.Update();
    }
}
