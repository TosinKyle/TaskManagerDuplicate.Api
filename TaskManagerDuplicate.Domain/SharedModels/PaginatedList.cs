

namespace TaskManagerDuplicate.Domain.SharedModels
{
     public class PaginatedList <T>   
     {
        public IEnumerable<T> Data { get; set; } //interface implemented by all collection,
        public PageMetaData MetaData { get; set; } //abstraction
     }
    public class PageMetaData 
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int TotalItems { get; set; }  //comes from the db cos value changes
        public int TotalPages { get; set; }
    }
}
