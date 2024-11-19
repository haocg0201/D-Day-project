using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Monster
{
    public override void Start()
    {
        base.Start();
        Initialize(monsterName: "Skeleton",
        health: 500,         
        attackDamage: 200,
        survivability: 1.5f,
        size: 1.5f,
        dialogues: new List<string>
        {
            "Hm Ghuuu ..!",
            " ?!",
            "Crak crakk"
        });
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

