using UnityEngine;

public class BoxingGloves : Weapon
{
    public override void Start()
    {
        base.Start();
        Initialize("boxingGloves","Găng tay boxing",25,3,1,"Võ thuật bất tận, nỗ lực không ngường nghỉ", 1f);
    }

    public override void Update(){
        base.Update();
    }
}
