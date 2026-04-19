using UnityEngine;

public class TorchEffect : ItemEffect
{
    [SerializeField] private float _torchDuration;
    [SerializeField] private float _torchLightIntensity;

    public float TorchDuration
    {
        get => _torchDuration;
    }
    public float TorchLightIntensity
    {
        get => _torchLightIntensity;
    }
    public override void OnUsed()
    {
        base.OnUsed();
        _inventoryChannel.RaiseTorchUsed(this);
    }
}
