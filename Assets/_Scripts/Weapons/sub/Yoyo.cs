using UnityEngine;

public class Yoyo : Weapon
{
    public override void Start()
    {
        base.Start();
        Initialize("yoyo","Yoyo siêu đẳng",15,5,1,"Sự kiên trì và nỗ lực sẽ đưa ta đi mọi nơi ta muốn.",0.5f);
    }

    public override void Update(){
        base.Update();
    }
}
