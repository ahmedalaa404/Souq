namespace Souq.Api.Helper
{
    public class ProductWithParam
    {
        public string? Sort { get; set; }


        public int? BrandId { get; set; }

        public int? TypeId { get; set; }


        private string? seach;

        public string? Search
        {
            get { return seach; }
            set { seach = string.IsNullOrEmpty(value)?string.Empty:value; }
        }



        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 5;






    }
}
