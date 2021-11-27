namespace Reminders.BibleBlazorCoreLibrary.ViewModels;
public class SimpleCheckReadingViewModel
{
    private readonly IBibleReadingContext _context;
    private readonly IBibleTimeClass _time;
    private readonly ISystemError _error;
    private readonly IToast _toast;
    private DailyReaderModel? _currentRead;
    public DateTime NextDate { get; private set; }
    public SimpleCheckReadingViewModel(IBibleReadingContext context, 
        IBibleTimeClass time,
        ISystemError error,
        IToast toast
        )
    {
        _context = context;
        _time = time;
        _error = error;
        _toast = toast;
    }
    public Action? NewReading { get; set; }
    public Action? FinishedReading { get; set; }
    private async Task CheckReadingsAsync()
    {
        _currentRead = await _context.GetCurrentReadingAsync();
        NextDate = await _time.GetNextReadingAsync(_currentRead);
        if (NextDate < DateTime.Now)
        {
            NewReading?.Invoke();
            return;
        }
        FinishedReading?.Invoke();
    }
    public async Task InitAsync()
    {
        try
        {
            bool rets = await _context.HasNewReadingAsync();
            if (rets == false)
            {
                NewReading?.Invoke();
                return;
            }
            await CheckReadingsAsync();
        }
        catch (Exception ex)
        {
            _error.ShowSystemError(ex.Message);
        }
    }
    public async Task CompleteReadingAsync()
    {
        await _context.CompletedReadingAsync(_currentRead!);
        bool rets = await _context.HasNewReadingAsync();
        if (rets == false)
        {
            _toast.ShowWarningToast("No more readings are left period.  May require a process to import readings");
            return;
        }
        await CheckReadingsAsync();
    }
}