using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRendererView : MonoBehaviour
{
    [System.Serializable]
    public class MaterialSet
    {
        public Material cooler;
        public Material baseMaterial;
        public Material baseTransparent;
    }

    [System.Serializable]
    public class RarityMaterials
    {
        public MaterialSet[] levels = new MaterialSet[5];
    }

    [Header("Materials by Rarity")]
    [SerializeField]
    private RarityMaterials common = new RarityMaterials();
    [SerializeField]
    private RarityMaterials rare = new RarityMaterials();
    [SerializeField]
    private RarityMaterials epic = new RarityMaterials();
    [SerializeField]
    private RarityMaterials legendary = new RarityMaterials();

    [SerializeField]
    private MeshRenderer baseMesh;

    [SerializeField]
    private List<MeshRenderer> coolersMesh = new List<MeshRenderer>();

    [SerializeField]
    private Transform cube;

    [SerializeField]
    private RotationImpulseController rotationImpulseController;

    private Material GetMaterial(string rarity, int level, string type)
    {
        RarityMaterials raritySet = rarity switch
        {
            "common" => common,
            "rare" => rare,
            "epic" => epic,
            "legendary" => legendary,
            _ => null
        };

        if (raritySet == null || level < 1 || level > 5) return null;

        MaterialSet materialSet = raritySet.levels[level - 1];
        return type switch
        {
            "Cooler" => materialSet.cooler,
            "Base" => materialSet.baseMaterial,
            "BaseTransparent" => materialSet.baseTransparent,
            _ => null
        };
    }


    public void SetToDefaultRotation()
    {
        rotationImpulseController.Stop();
        cube.rotation = Quaternion.Euler(-90, 160, 0);
    }

    public void ShowCube(string rarity, int level)
    {
        Material coolerMaterial = GetMaterial(rarity, level, "Cooler");
        Material baseMaterial = GetMaterial(rarity, level, "Base");
        Material baseTransparentMaterial = GetMaterial(rarity, level, "BaseTransparent");

        baseMesh.SetSharedMaterials(new List<Material>() { baseMaterial, baseTransparentMaterial });

        foreach (MeshRenderer cooler in coolersMesh)
        {
            cooler.sharedMaterial = coolerMaterial;
        }
    }
}
