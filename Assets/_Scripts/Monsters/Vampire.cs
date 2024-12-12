using System.Collections.Generic;
using UnityEngine;

public class Vampire : Monster
{
    public override void Start()
    {
        base.Start();
        Initialize("Vampire",2500,200,1.5f,1.8f,new List<string>{"!!"," Blood"," Geggg"},5);
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
