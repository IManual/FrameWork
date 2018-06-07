using UnityEditor;
using System;

public sealed class ProgressIndicator : IDisposable
{
    private string text;

    private float total = 1f;

    private float progress;

    private double doubleNum1 = 0.03;

    private double doubleNum2;

    public ProgressIndicator(string title)
    {
        text = title;
    }

    public void SetTotal(float total)
    {
        this.total = total;
    }

    public void AddProgress(float count = 1f)
    {
        this.progress += count;
    }

    public void SetProgress(float progress)
    {
        this.progress = progress;
    }

    public bool Show(string message)
    {
        double num = this.doubleNum2;
        this.doubleNum2 = EditorApplication.timeSinceStartup;
        if (this.doubleNum2 - num <= this.doubleNum1)
        {
            return false;
        }
        float num2 = this.progress / this.total;
        return EditorUtility.DisplayCancelableProgressBar(text, message, num2);
    }

    public bool Show(string format, params object[] args)
    {
        return this.Show(string.Format(format, args));
    }

    public bool ShowForce(string message)
    {
        float num = this.progress / this.total;
        return EditorUtility.DisplayCancelableProgressBar(text, message, num);
    }

    public bool ShowForce(string format, params object[] args)
    {
        return this.ShowForce(string.Format(format, args));
    }

    public void Dispose()
    {
        EditorUtility.ClearProgressBar();
    }
}
