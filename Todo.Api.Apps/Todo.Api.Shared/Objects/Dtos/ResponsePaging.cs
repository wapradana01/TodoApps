﻿using Todo.Api.Shared.Enums;
using Todo.Api.Shared.Helpers;

namespace Todo.Api.Shared.Objects.Dtos
{
    public class ResponsePagingBase(string? message = null, ResponseCode responseCode = ResponseCode.BadRequest) : ResponseBase(message, responseCode)
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public int RecordsFiltered { get; set; }
        public int RecordsTotal { get; set; }
        public bool HasNext => Page < TotalPage && TotalPage > 1;
        public bool HasPrevious => Page > 1;
    }

    public class ResponsePaging<T>(string? message = null, ResponseCode responseCode = ResponseCode.BadRequest) : ResponsePagingBase(message, responseCode)
        where T : class
    {
        public IQueryable<T>? Data { get; set; }

        public void ApplyPagination(int page, int pageSize, IQueryable<T>? obj, string? message = null)
        {
            Page = page;
            PageSize = pageSize;
            RecordsFiltered = obj?.Count() ?? 0;
            RecordsTotal = obj?.Count() ?? 0;
            TotalPage = PagingHelper.CalculateTotalPage(RecordsTotal, PageSize);
            Data = obj?.PaginateQuery(Page, pageSize);

            OK(message);
        }

        public void CopyPaginationInfo(ResponsePagingBase responsePagingSource, IQueryable<T>? obj, string? message = null)
        {
            Page = responsePagingSource.Page;
            PageSize = responsePagingSource.PageSize;
            RecordsFiltered = responsePagingSource.RecordsFiltered;
            RecordsTotal = responsePagingSource.RecordsTotal;
            TotalPage = responsePagingSource.TotalPage;
            Data = obj;

            OK(message);
        }
    }
}
