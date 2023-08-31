using UnityEngine;

namespace Services
{
    public class MeshCombineService : IMeshCombineService
    {
        public void Combine(GameObject gameObject, bool useMeshCollider = false)
        {
            MeshFilter[] meshFilters = gameObject.GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];

            for (int i = 0; i < meshFilters.Length; i++)
            {
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = gameObject.transform.worldToLocalMatrix * meshFilters[i].transform.localToWorldMatrix;
                if (i > 0 && meshFilters[i] != null)
                {
                    Object.Destroy(meshFilters[i].gameObject);
                }
            }

            Mesh mesh = new Mesh();
            mesh.CombineMeshes(combine);
            gameObject.transform.GetComponent<MeshFilter>().sharedMesh = mesh;

            MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
            meshCollider.convex = true;
        }
    }
}