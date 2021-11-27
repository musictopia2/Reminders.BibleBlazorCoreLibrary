namespace Reminders.BibleBlazorCoreLibrary.DataAccess;
public class BibleContent : IBibleContent
{
    Task<BasicList<string>> IBibleContent.GetBookChaperDataAsync(string bookName, int chapter)
    {
        using BibleContext bibs = new();
        return bibs.GetVersesAsync(bookName, chapter);
    }
}