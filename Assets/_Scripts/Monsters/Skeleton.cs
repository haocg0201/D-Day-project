using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Monster
{
    public override void Start()
    {
        base.Start();
        Initialize(monsterName: "Skeleton",
        health: 5000,         
        attackDamage: 800,
        survivability: 1.5f,
        size: 1.5f,
        dialogues: new List<string>
        {
            "Hm Ghuuu ..!",
            " ?!",
            "Crak crakk"
        },2);
        transform.localScale = new Vector3(size, size,size);

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

