using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerDuplicate.Domain.SharedModels;

namespace TaskManagerDuplicate.Helper
{
     public class PaginationHelper<T>
     {
        private static PageMetaData BuildMetaData(int page, int perPage,int totalItems)   //to be called on by
        {
            int totalPages = totalItems%perPage ==0? totalItems/perPage:totalItems/perPage+1;   //modulus operator incase it results ti dec
            return new PageMetaData { 
                 Page = page,
                  PerPage = perPage,
                  TotalPages = totalPages,
                  TotalItems = totalItems,
            };
        }
        public static PaginatedList<T> Paginate(List<T> data, int perPage, int page) 
        {
         int offset= (page-1)*perPage;
         var response = data.Skip(offset).Take(perPage).ToList();
            return new PaginatedList<T>
            {
                Data = response,
                MetaData = BuildMetaData(page, perPage, data.Count)
            };
        }
     
     }
}
