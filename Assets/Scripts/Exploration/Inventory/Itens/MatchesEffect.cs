public class MatchesEffect : ItemEffect
{
    public override void OnUsed()
    {
        _inventoryChannel.RaiseMatchesUsed();
    }
}
