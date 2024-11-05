using NUnit.Framework;
using UnityEngine;
public class SpawnSkeleton :MonoBehaviour 
{
    private string mName;
    // Prefabs Skeleton
    // tạo danh sách quản lý quá sinh
    private void Start()
    {
        Skeleton skeleton = new Skeleton();
        string mNane = skeleton.monsterName;
    }

    
    private void Amount()
    {
        // int soLuongSinh = 30; tiêu diệt diệt 1 quái -1 con
        // sinh quái ra
        // sinh xong add vào danh sách để quản lý
        // 
    }
}
