namespace Reminders.BibleBlazorCoreLibrary.Components;
public partial class SimpleCheckReadingComponent
{
    private enum EnumStatus
    {
        Starting,
        ShowReading,
        FinishedReading
    }
    private EnumStatus _status = EnumStatus.Starting;
    [Inject]
    private SimpleCheckReadingViewModel? DataContext { get; set; }
    private async Task CheckNextReadingAsync()
    {
        _status = EnumStatus.Starting;
        await DataContext!.CompleteReadingAsync();
    }
    protected override async Task OnInitializedAsync()
    {
        DataContext!.FinishedReading = () =>
        {
            _status = EnumStatus.FinishedReading;
            StateHasChanged();
        };
        DataContext.NewReading = () =>
        {
            _status = EnumStatus.ShowReading;
            StateHasChanged();
        };
        await DataContext!.InitAsync();
    }
}