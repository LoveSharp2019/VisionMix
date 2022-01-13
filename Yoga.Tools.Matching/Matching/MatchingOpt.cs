using System.Collections.Generic;

namespace Yoga.Tools.Matching
{
    public delegate void StatisticsDelegate(MatchingStatus val);
	
    /// <summary>
    /// This class and its derived classes MatchingOptSpeed and 
    /// MatchingOptStatistics implement the optimization process for the 
    /// matching parameters in terms of the recognition speed and the 
    /// recognition rate. Similar to the processing in HDevelop, a timer 
    /// is used to be able to abort the processing during a run.
    /// </summary>
    public class MatchingOpt
	{
       
        /// <summary>
        /// Delegate to notify about the state of  the optimization process
        /// </summary>
		public	StatisticsDelegate NotifyStatisticsObserver;
		/// <summary>
		/// Information about the optimization process 
		/// (e.g. Success or Failure) to be displayed in the GUI
		/// </summary>
        public string StatusString { get; protected set; }		
        /// <summary>
        /// Statistics for the parameter optimization
        /// </summary>
		public string [] RecogTabOptimizationData = new string[8];
		/// <summary>
		/// Statistics for the recognition rate
		/// </summary>
        public string [] InspectTabRecogRateData  = new string[5];
		/// <summary>
		/// Statistics of detection results for the optimal 
		/// recognition rate
		/// </summary>
        public string [] InspectTabStatisticsData = new string[21];

        /// <summary>
        /// Reference to instance of MatchingAssistant, 
        /// which triggers the optimization performance.
        /// </summary>
		protected MatchingTool	matchingAssistant;
        /// <summary>
        /// Result of detection 
        /// </summary>
		protected MatchingResult		matchingResults;
        /// <summary>
        /// Number of all test images to be inspected
        /// </summary>
		protected int					imageCount;
        /// <summary>
        /// Index of test image, being inspected currently
        /// </summary>
		protected int				 	currentIndex;
        /// <summary>
        /// Flag, indicating success or failure of optimization process
        /// </summary>
		protected bool					optSuccess;

        protected IEnumerator<string> iterator;

		/// <summary> 
		/// ¹¹Ôìº¯Êý
		/// </summary>
		public MatchingOpt(){}

		/// <summary>  
		/// Performs an optimization step.
		/// </summary>
        public virtual bool ExecuteStep(){ return true;	}

		/// <summary>  
		/// Resets all parameters for evaluating the performance to their initial values.
		/// </summary>
		public virtual void Reset(){}

        /// <summary>
        /// 
        /// </summary>
		public virtual void Stop(){}
        
        public void dummy(MatchingStatus val) { }
	}//class
}//end of namespace
