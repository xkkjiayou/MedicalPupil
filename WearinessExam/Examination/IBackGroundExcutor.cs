namespace WearinessExam.Examination
{
    /// <summary>
    /// Delegate Completed
    /// </summary>
    /// <param name="success">if set to <c>true</c> [success].</param>
    /// <param name="originalData">The original data.</param>
    /// <param name="examData">The exam data.</param>
    public delegate void Completed(bool success, object originalData, object examData);

    /// <summary>
    /// Interface IBackGroundExcutor
    /// </summary>
    public interface IBackGroundExcutor
    {
        /// <summary>
        /// Starts this instance.
        /// </summary>
        bool Start();
        /// <summary>
        /// Stops this instance.
        /// </summary>
        bool Stop();
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error message.</value>
        string ErrorMessage { get; set; }
        /// <summary>
        /// Occurs when [completed handler].
        /// </summary>
        event Completed CompletedHandler;
    }
}
