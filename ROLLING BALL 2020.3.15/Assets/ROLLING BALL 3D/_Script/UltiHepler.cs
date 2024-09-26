using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiHepler {
    //get random with weighted object
    public static int GetRandomWeightedIndex(List<float> weights)
    {
        if (weights == null || weights.Count == 0) return -1;

        float w = 0;
        float t = 0;
        int i;
        for (i = 0; i < weights.Count; i++)
        {
            w = weights[i];
            if (float.IsPositiveInfinity(w)) return i;
            else if (w >= 0f && !float.IsNaN(w)) t += weights[i];
        }

        float r = Random.value;
        float s = 0f;

        for (i = 0; i < weights.Count; i++)
        {
            w = weights[i];
            if (float.IsNaN(w) || w <= 0f) continue;

            s += w / t;
            if (s >= r) return i;
        }

        return -1;
    }

    public static int GetRandomWeightedIndex(float[] weights)
    {
        if (weights == null || weights.Length == 0) return -1;

        float w = 0;
        float t = 0;
        int i;
        for (i = 0; i < weights.Length; i++)
        {
            w = weights[i];
            if (float.IsPositiveInfinity(w)) return i;
            else if (w >= 0f && !float.IsNaN(w)) t += weights[i];
        }

        float r = Random.value;
        float s = 0f;

        for (i = 0; i < weights.Length; i++)
        {
            w = weights[i];
            if (float.IsNaN(w) || w <= 0f) continue;

            s += w / t;
            if (s >= r) return i;
        }

        return -1;
    }
}
