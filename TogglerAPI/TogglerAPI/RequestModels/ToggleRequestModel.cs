namespace TogglerAPI.RequestModels
{
    public class ToggleRequestModel
    {
        public int? ToggleId { get; set; }
        public string Name { get; set; }
        public bool Value { get; set; }
    }
}
