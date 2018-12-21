using System.ComponentModel;

namespace MedicalSys.MSCommon
{
    public interface IBackGroundWorkerObject
    {
       // void DoWork(object sender, DoWorkEventArgs e);
        BackgroundWorker BackgroundWorker { get; set; }
        void DoWork();
        string ProccessingMessage { get; }
        string CompletedMessage { get; }
        string ErrorMessage { get; set; }
    }
}
