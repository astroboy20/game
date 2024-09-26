using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelPath : MonoBehaviour {
    public MeshRenderer textMesh;
    public Texture[] levelTxts;

    public void Init(int level)
    {
        textMesh.material.mainTexture = levelTxts[level - 1];
    }
}
