using UnityEngine;

public enum EUpgrade
{
    Trabalhadores, Fazenda, Barcos
}

[CreateAssetMenu]
public class UpgradeData : ScriptableObject
{
    public string Name;
    public string Description;
    public EUpgrade Upgrade;
    public Sprite PortraitImage;
}