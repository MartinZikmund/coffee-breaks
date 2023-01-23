using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Timers;

namespace UnoTimers;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
    }

    private void StartTimer()
    {
        //StartThreadingTimer();
        //StartTimersTimer();
        //StartDispatcherTimer();
        StartDispatcherQueueTimer();
    }

    private void StopTimer()
    {
        //StopThreadingTimer();
        //StopTimersTimer();
        //StopDispatcherTimer();
        StopDispatcherQueueTimer();
    }

    #region System.Threading.Timer

    private System.Threading.Timer _threadingTimer;

    private void StartThreadingTimer()
    {
        _threadingTimer ??= new System.Threading.Timer(ThreadingTimerCallback, state: null, TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(500));
    }

    private void ThreadingTimerCallback(object state)
    {
        DispatcherQueue.TryEnqueue(() =>
        {
            TimeTextBlock.Text = DateTime.UtcNow.ToString("HH:mm:ss.fff");
        });
    }

    private async void StopThreadingTimer()
    {
        await _threadingTimer.DisposeAsync();
        _threadingTimer = null;
    }

    #endregion

    #region System.Timers.Timer

    private System.Timers.Timer _timersTimer;

    private void StartTimersTimer()
    {
        _timersTimer ??= new System.Timers.Timer(TimeSpan.FromMilliseconds(500));
        _timersTimer.Elapsed += TimersTimerElapsed;
        _timersTimer.Start();
    }

    private void TimersTimerElapsed(object sender, ElapsedEventArgs e)
    {
        DispatcherQueue.TryEnqueue(() =>
        {
            TimeTextBlock.Text = DateTime.UtcNow.ToString("HH:mm:ss.fff");
        });
    }

    private void StopTimersTimer()
    {
        _timersTimer.Stop();
        _timersTimer = null;
    }

    #endregion

    #region DispatcherTimer

    private DispatcherTimer _dispatcherTimer;

    private void StartDispatcherTimer()
    {
        _dispatcherTimer ??= new DispatcherTimer();
        _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(500);
        _dispatcherTimer.Tick += DispatcherTimerTick;
        _dispatcherTimer.Start();
    }

    private void DispatcherTimerTick(object sender, object e)
    {
        TimeTextBlock.Text = DateTime.UtcNow.ToString("HH:mm:ss.fff");
    }

    private void StopDispatcherTimer()
    {
        _dispatcherTimer.Stop();
        _dispatcherTimer = null;
    }

    #endregion

    #region DispatcherQueueTimer

    private DispatcherQueueTimer _dispatcherQueueTimer;

    private void StartDispatcherQueueTimer()
    {
        _dispatcherQueueTimer ??= DispatcherQueue.CreateTimer();
        _dispatcherQueueTimer.Interval = TimeSpan.FromMilliseconds(500);
        _dispatcherQueueTimer.Tick += DispatcherQueueTimerTick;
        _dispatcherQueueTimer.Start();
    }

    private void DispatcherQueueTimerTick(DispatcherQueueTimer sender, object args)
    {
        TimeTextBlock.Text = DateTime.UtcNow.ToString("HH:mm:ss.fff");
    }

    private void StopDispatcherQueueTimer()
    {
        _dispatcherQueueTimer.Stop();
        _dispatcherQueueTimer = null;
    }

    #endregion
}
