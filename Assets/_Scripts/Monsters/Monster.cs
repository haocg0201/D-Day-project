
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    // protected - private - public tùy chỉnh theo nhu cầu nhé huhu
    public string monsterName;
    public int health;
//    public float mDef;           // -- giáp
    public int maxHealth;
    public int attackDamage;
    public float survivability;
    public float size;
    public List<string> dialogues; // gồm 3 thoại tối thiểu
    public Skill mSkill;
    //protected List<AnimationClip> animations;

    public Monster(string monsterName, int health, int attackDamage, float survivability, float size, List<string> dialogues, Skill mSkill)
    {
        this.monsterName = monsterName;
        this.health = health;
        this.maxHealth = health;
        this.attackDamage = attackDamage;
        this.survivability = survivability;
        this.size = size;
        this.dialogues = dialogues;
        this.mSkill = mSkill;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health < 0) health = 0;
        if(health > maxHealth) health = maxHealth;
        if (health <= 0) Die();
    }

    public int Die()
    {
        return -1; // quái cook XD
    }

    public virtual void SkillConsumption()
    {
            // do someting
    }

    public virtual string Idle()
    {
        return dialogues[0];
    }

    public virtual string Run()
    {
        return dialogues[1];
    }

    public virtual string Attack()
    {
        return dialogues[2];
    }
}
