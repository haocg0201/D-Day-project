using System.Collections.Generic;
using UnityEngine;

public class Zombie : Monster
{
    
    public override void Start()
    {
        base.Start();
        Initialize("Zombie", 1000, 500, 1f, 1.6f, new List<string> { "uhhh owhh", "?!", "Gawww wwwoo" });
        transform.localScale = new Vector3(size, size, size);
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
