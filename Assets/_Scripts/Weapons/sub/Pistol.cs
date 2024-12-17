using UnityEngine;

public class Pistol : Weapon
{
    public override void Start()
    {
        base.Start();
        Initialize("pistol","Súng lục cảnh sát",10,1,1.5f,"Thực thi trật tự, thực thi công lý.",0.4f);
    }

    public override void Update(){
        base.Update();
    }
}
