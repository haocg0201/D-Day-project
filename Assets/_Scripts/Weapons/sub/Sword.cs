using UnityEngine;

public class Sword : Weapon
{
    public override void Start()
    {
        base.Start();
        Initialize("sword","Kiếm cùn",10,5,1,"Ta đã từng là một thanh kiếm mạnh nhất.",0.7f);
    }

    public override void Update(){
        base.Update();
    }
}
