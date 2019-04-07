namespace NHT.ASM.Data.EntityData
{
    public partial class DummyData
    {
        protected int? StartId { get; set; }

        public DummyData(int? startId)
        {
            // data initialization must be in sequence otherwise it will break

            StartId = startId;

            SetUserTypes();
            SetUser();
        }
    }
}