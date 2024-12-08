using System.Collections.Generic;
using UnityEngine;

public class Slime : Monster
{
    public override void Start()
    {
        base.Start();
        Initialize("Slime",500,20,1f,0.3f,new List<string>{":))"," ?!"," @"},3);
        //transform.localScale = new Vector3(size, size, size);
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
