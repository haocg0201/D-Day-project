using System.Collections.Generic;
using UnityEngine;

public class Troll : Monster
{
    public override void Start()
    {
        base.Start();
        Initialize("Troll",5000,1000,2f,4f, new List<string>{"uglllyyy"," ?!","shff shff"});
        transform.localScale = new Vector3(size,size,size);
    }

    public override void Update()
    {
        base.Update();
    }
    public override void SkillConsumption()
    {
        // t�nh sau
    }
}
