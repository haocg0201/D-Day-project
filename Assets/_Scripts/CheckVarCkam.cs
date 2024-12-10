using System;
using System.Collections.Generic;
using UnityEngine;

public class CheckVarCkam : MonoBehaviour
{
    private Dictionary<string, Action<Collider2D>> tagActions;
    private Player player;

    void Start()
    {
        // Khởi tạo từ điển ánh xạ Tag -> Hành động
        tagActions = new Dictionary<string, Action<Collider2D>>()
        {
            { "MXtal", HandleMXtal },
            { "RXtal", HandleRXtal },
            { "HP", HandleHP },
            { "LP", HandleLP },
            { "SP", HandleSP },
            { "DP", HandleDP }
        }; // đăng ký vật phẩm va chạm ở đây ae
        player = GetComponentInParent<Player>();
        if(player == null){
            player = Player.Instance;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.Instance != null && tagActions.TryGetValue(other.tag, out Action<Collider2D> action))
        {
            action.Invoke(other);
            Destroy(other.gameObject);
        }
    }

    // Các hành động cụ thể
    private void HandleMXtal(Collider2D other)
    {
        GameManager.Instance._mgCounter+=1;
    }

    private void HandleRXtal(Collider2D other)
    {
        GameManager.Instance._rgCounter+=1;
    }

    private void HandleHP(Collider2D other)
    {
        if(player == null) return;
        player.GetHealBuff(200);
    }

    private void HandleLP(Collider2D other)
    {
        // life potion = mana tạo sau nhé
    }

    private void HandleSP(Collider2D other)
    {
        //Debug.Log("Collided with SP!");
    }

    private void HandleDP(Collider2D other)
    {
        //Debug.Log("Collided with DP!");
    }
}
