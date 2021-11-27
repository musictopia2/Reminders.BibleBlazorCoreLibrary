namespace Reminders.BibleBlazorCoreLibrary.ViewModels;
public class ReaderViewModel
{
    public string BookName { get; set; } = "";
    //eventually can be more than one book though.  think about that for future version.
    private readonly IBibleReadingContext _readingContext;
    private readonly IBibleContent _content;
    public BasicList<string> Verses { get; set; } = new();
    public ReaderViewModel(IBibleReadingContext readingContext, IBibleContent content)
    {
        _readingContext = readingContext;
        _content = content;
    }
    public async Task InitAsync()
    {
        DailyReaderModel daily = await _readingContext.GetCurrentReadingAsync();
        BookName = daily.Book;
        Verses = await _content.GetBookChaperDataAsync(daily.Book, daily.Chapter);
    }
}