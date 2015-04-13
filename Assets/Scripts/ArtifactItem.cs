using UnityEngine;
using System.Collections;

public class ArtifactItem : PickupableItem
{

    public ArtifactType artifactType;


    public override void PickupItem(Inventory inventory)
    {
        inventory.AddArtifact(artifactType);
        GlobalSettings.SendMessage(MessageType.artifact);
    }
}
