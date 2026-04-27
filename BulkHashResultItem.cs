using HashGenerator.VIewModel;

namespace HashGenerator.Model
{
    public class BulkHashResultItem : ViewModelBase
    {
        public int BulkSrNo { get; set; }
        public string BulkOriginalFileName { get; set; }
        public string BulkGeneratedFileName { get; set; }
        public string BulkBranch { get; set; }
        public string BulkPassKey { get; set; }
        public string BulkFileHashValue { get; set; }
    }


}
