using System.Collections.Generic;
using UnityEngine;

public class Werewolf : Monster
{
    public override void Start()
    {
        base.Start();
        Initialize("Werewolf",2000,50, 1.5f,2.0f,new List<string>{"auwww wuuu"," ?!","waiiiuu"},6);
        transform.localScale = new Vector3(size,size,size);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void SkillConsumption()
    {
        // tính sau
    }
}
