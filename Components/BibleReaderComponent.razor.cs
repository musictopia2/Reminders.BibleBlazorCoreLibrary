namespace Reminders.BibleBlazorCoreLibrary.Components;
public partial class BibleReaderComponent
{
    [Parameter]
    public EventCallback FinishedReading { get; set; }
    //this means that anybody can decide what to do after you finished the reading.
    [Inject]
    private ReaderViewModel? DataContext { get; set; }
    private bool _loading = true;
    private readonly ReaderModel _tempModel = new();
    protected override async Task OnInitializedAsync()
    {
        await DataContext!.InitAsync();
        _loading = false;
    }
}