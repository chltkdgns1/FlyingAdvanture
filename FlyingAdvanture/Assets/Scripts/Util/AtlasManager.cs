using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class AtlasManager : MonoSingleTon<AtlasManager>
{
    public SpriteAtlas inGameAtlas;
    public SpriteAtlas lobbyAtlas;
    public SpriteAtlas purchaseAtals;

    protected override void Init()
    {
        inGameAtlas = Resources.Load("Atlas/InGameScene") as SpriteAtlas;
        inGameAtlas = Resources.Load("Atlas/LobbyAtlas") as SpriteAtlas;
        inGameAtlas = Resources.Load("Atlas/PurchaseAnumAtlas") as SpriteAtlas;
    }

    public void OnStart() { }
}
