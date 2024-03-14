using UnityEngine;

public class SkinPlayerApply : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer playerRenderer;
    void Start()
    {
        Material skin = SkinManager.GetInstance().GetSkin();
        Debug.Log(skin);

        // Wende das Material auf den Spieler an
        Material[] playerMaterials = playerRenderer.materials;
        playerMaterials[0] = skin;
        playerRenderer.materials = playerMaterials;
    }
}