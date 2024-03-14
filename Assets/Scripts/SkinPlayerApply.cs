using UnityEngine;

public class SkinPlayerApply : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer playerRenderer;
    void Start()
    {
        /*
        // Lese den ausgewählten Skin-Index und das Material aus den PlayerPrefs
        int selectedSkinIndex = PlayerPrefs.GetInt("SelectedSkinIndex", 0);
        string selectedSkinMaterialName = PlayerPrefs.GetString("SelectedSkinMaterial", "");

        // Wende das Material auf den Spieler an
        Material[] playerMaterials = playerRenderer.materials;
        playerMaterials[0] = FindMaterialByName(selectedSkinMaterialName);
        playerRenderer.materials = playerMaterials;
        */
    }

    Material FindMaterialByName(string materialName)
    {
        // Finde das Material im Assets-Ordner nach dem Namen
        return Resources.Load<Material>(materialName);
    }
}