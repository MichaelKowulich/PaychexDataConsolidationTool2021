using System;
using System.Threading.Tasks;

public class TableLogic
{
    public int totalPages;
    public int totalRecords;
    public int curPage;
    public int pagerSize;
    public int pageSize;
    public int startPage;
    public int endPage;
    public string sortColumnName = "DateOfReport";
    public string sortDir = "ASC";
    public bool isSortedAscending;
    public string activeSortColumn;

    public TableLogic(int totalPages, int totalRecords, int curPage, int pagerSize, int pageSize, int startPage, string sortColumnName, string sortDir)
    {
        this.totalPages = totalPages;
        this.totalRecords = totalRecords;
        this.curPage = curPage;
        this.pagerSize = pagerSize;
        this.pageSize = pageSize;
        this.startPage = startPage;
        this.sortColumnName = sortColumnName;
        this.sortDir = sortDir;
    }


    //////////////////////////////////////////////////////////////////////////
    //
    // This controls the forward and backward motion of tabular page turning
    //
    public void SetPagerSize(string direction)
    {
        if (direction == "forward" && endPage < totalPages)
        {
            startPage = endPage + 1;
            if (endPage + pagerSize < totalPages)
            {
                endPage = startPage + pagerSize - 1;
            }
            else
            {
                endPage = totalPages;
            }
            //this.StateHasChanged();
        }
        else if (direction == "back" && startPage > 1)
        {
            endPage = startPage - 1;
            startPage = startPage - pagerSize;
        }
        else
        {
            startPage = 1;
            endPage = totalPages;
        }
    }

    ////////////////////////////////////////////////////////////////////
    //
    // Setting the icon to be either ^ v because we deserve nice things
    //
    public string SetSortIcon(string columnName)
    {
        if (activeSortColumn != columnName)
        {
            return string.Empty;
        }
        if (isSortedAscending)
        {
            return "fa-sort-up";
        }
        else
        {
            return "fa-sort-down";
        }
    }

}
