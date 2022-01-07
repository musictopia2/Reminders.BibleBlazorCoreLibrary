namespace Reminders.BibleBlazorCoreLibrary.DataAccess;
public class BibleContent : IBibleContent
{
    private readonly IBookDataService _book;
    private readonly ITranslationService _translation;
    public BibleContent(IBookDataService book, ITranslationService translation) //this means this needs to be registered via di for bookservice and translationservice.
    {
        //this means this does not require a dependency on the content anymore.
        _book = book;
        _translation = translation;
    }
    Task<BasicList<string>> IBibleContent.GetBookChaperDataAsync(string bookName, int chapter)
    {
        using BibleContext bibs = new(_book, _translation);
        return bibs.GetVersesAsync(bookName, chapter);
    }
}