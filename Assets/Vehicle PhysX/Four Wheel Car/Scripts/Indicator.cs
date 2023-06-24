using UnityEngine;

public class Indicator : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private bool emissionEnabled;
    [ColorUsage(false, true)]
    [SerializeField] private Color colorOffState;
    [ColorUsage(false, true)]
    [SerializeField] private Color colorOnState;

    private Material material;

    private void Start()
    {
        material = meshRenderer.material;

        material.EnableKeyword("_EMISSION");
    }

    private void Update()
    {
        material.SetColor("_EmissionColor", emissionEnabled ? colorOnState : colorOffState);
    }
}
