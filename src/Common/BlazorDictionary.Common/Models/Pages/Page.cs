﻿namespace BlazorDictionary.Common.Models.Pages;

public class Page
{
    public Page() : this(0) { }
    public Page(int totalRowCount) : this(1, 10, totalRowCount) { }
    public Page(int pageSize, int totalRowCount) : this(1, pageSize, totalRowCount) { }
    public Page(int currentPage, int pageSize, int totalRowCount)
    {
        if (currentPage < 1)
        {
            throw new ArgumentOutOfRangeException("Invalid Page Number");
        }

        if (pageSize < 1)
        {
            throw new ArgumentOutOfRangeException("Invalid Page Size");
        }

        TotalRowCount = totalRowCount;
        CurrentPage = currentPage;
        PageSize = pageSize;
    }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalRowCount { get; set; }
    public int TotalPageCount
    {
        get
        {
            return (int)Math.Ceiling((double)TotalRowCount / PageSize);
        }
    }

    public int Skip
    {
        get
        {
            return (CurrentPage - 1) * PageSize;
        }
    }
}
