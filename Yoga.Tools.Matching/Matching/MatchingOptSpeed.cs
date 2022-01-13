using System;


namespace Yoga.Tools.Matching
{
    /// <summary>
    /// This class optimizes the performance of a defined shape-based model
    /// for a given set of test images.
    /// To perform an optimization of the detection parameters, the instance
    /// has to know the used set of matching parameters and the calling 
    /// MatchingAssistant, to retrieve the set of test images and to call 
    /// the methods for finding the model. 
    /// The optimization is performed in the sense that the two detection 
    /// parameters ScoreMin and Greediness are iteratively increased and
    /// decreased, respectively, and every new parameter combination is used
    /// to detect the model in the set of test images. Each performance is
    /// then measured and compared with the best performance so far.
    /// The single execution steps are triggered by a timer from the
    /// class MatchingAssistant, so that you can stop the optimization anytime
    /// during the run.
    /// </summary>
   	public class MatchingOptSpeed : MatchingOpt
    {
        // private class members 
        private int mCurrScoreMin;
        private int mCurrGreediness;
        private double mCurrMeanTime;
        private int mScoreMinStep;
        private int mGreedinessStep;

        private int mOptScoreMin;
        private int mOptGreediness;
        private double mOptMeanTime;

        private int mMatchesNum;
        private int mExpMatchesNum;


        /// <summary>Constructor</summary>
        /// <param name="mAss">MatchingAssistant that created this instance</param>
        /// <param name="mPars">Current set of matching parameters</param>
        public MatchingOptSpeed(MatchingTool mAss)
        {
            matchingAssistant = mAss;
            NotifyStatisticsObserver = new StatisticsDelegate(dummy);

            mScoreMinStep = -10;
            mGreedinessStep = 10;
            Reset();

            imageCount = matchingAssistant.TestImageDic.Count;
        }


        /// <summary>
        /// In each execution step a certain parameter set is applied 
        /// to the whole set of test images and the performance is then
        /// evaluated.
        /// </summary>
        public override bool ExecuteStep()
        {
            double cScoreMin, cGreed, recogRate;
            string fileName;
            int actualMatches, expectedMatches;
            bool success;

            if (!iterator.MoveNext())
                return false;


            cScoreMin = mCurrScoreMin / 100.0;
            cGreed = mCurrGreediness / 100.0;

            StatusString = "测试图片 " + (currentIndex + 1) +
                            "  - 最小分数:  " + cScoreMin +
                            "  - 贪心算法:  " + cGreed;

            NotifyStatisticsObserver(MatchingStatus.UpdateRecogSattisticStatus);

            fileName = iterator.Current;

            matchingAssistant.SetTestImage(fileName);

            matchingAssistant.SetMinScore(cScoreMin);
            matchingAssistant.SetGreediness(cGreed);

            if (!matchingAssistant.ApplyFindModel())
                return false;


            matchingResults = matchingAssistant.MatchingResults;
            actualMatches = matchingResults.Count;

            expectedMatches = 0;

            switch (matchingAssistant.ParameterSet.RecogSpeedMode)
            {
                case MatchingParam.RECOGM_MANUALSELECT:
                    expectedMatches = matchingAssistant.ParameterSet.RecogManualSel;
                    break;
                case MatchingParam.RECOGM_ATLEASTONE:
                    expectedMatches = 1;
                    if (actualMatches > 1)
                        actualMatches = 1;
                    break;
                case MatchingParam.RECOGM_MAXNUMBER:
                    expectedMatches = matchingAssistant.ParameterSet.NumMatches;
                    break;
                default:
                    break;
            }

            mMatchesNum += actualMatches;
            mExpMatchesNum += expectedMatches;


            recogRate = (mExpMatchesNum > 0) ?
                        (100.0 * mMatchesNum / mExpMatchesNum) : 0.0;

            mCurrMeanTime = mCurrMeanTime * currentIndex + matchingResults.TimeFound;
            mCurrMeanTime /= ++currentIndex;

            //write data into strings and call for update
            RecogTabOptimizationData[0] = "" + Math.Round(cScoreMin, 2);
            RecogTabOptimizationData[1] = "" + Math.Round(cGreed, 2);
            RecogTabOptimizationData[2] = "" + Math.Round(recogRate, 2) + " %";

            if (mCurrMeanTime < 1000.0)
                RecogTabOptimizationData[3] = Math.Round(mCurrMeanTime, 2) + "  ms";
            else
                RecogTabOptimizationData[3] = Math.Round(mCurrMeanTime / 1000.0, 2) + "  s";

            NotifyStatisticsObserver(MatchingStatus.UpdateRecogVals);

            if (currentIndex < imageCount)
                return true;

            iterator.Reset();
            currentIndex = 0;
            mMatchesNum = 0;
            mExpMatchesNum = 0;

            success = (matchingAssistant.ParameterSet.RecogRateOpt == 0) ?
                          (Math.Abs(recogRate - matchingAssistant.ParameterSet.RecogRate) < 0.001)
                        : (recogRate >= (matchingAssistant.ParameterSet.RecogRate - 0.000001));


            if (success)
            {
                optSuccess = true;
                if (mCurrMeanTime < mOptMeanTime)
                {
                    mOptScoreMin = mCurrScoreMin;
                    mOptGreediness = mCurrGreediness;

                    this.RecogTabOptimizationData[4] = "" + Math.Round(mOptScoreMin / 100.0, 2);
                    this.RecogTabOptimizationData[5] = "" + Math.Round(mOptGreediness / 100.0, 2);
                    this.RecogTabOptimizationData[6] = Math.Round(recogRate, 2) + " %";

                    mOptMeanTime = mCurrMeanTime;
                    RecogTabOptimizationData[7] = RecogTabOptimizationData[3];
                    NotifyStatisticsObserver(MatchingStatus.UpdateRecogOptimumVals);
                }
                mCurrGreediness += mGreedinessStep;
                return (mCurrGreediness <= 100);
            }

            mCurrScoreMin += mScoreMinStep;

            if (optSuccess)
                return (mCurrScoreMin >= 10);

            return (mCurrScoreMin > 0);
        }


        /// <summary>
        /// Resets all parameters for evaluating the performance to their initial values.
        /// </summary>
        public override void Reset()
        {
            optSuccess = false;

            for (int i = 0; i < 8; i++)
                this.RecogTabOptimizationData[i] = "-";

            StatusString = "优化状态:";

            mCurrScoreMin = 100;
            mCurrGreediness = 0;
            mCurrMeanTime = 0.0;

            mOptScoreMin = 100;
            mOptGreediness = 0;
            mOptMeanTime = Double.MaxValue;

            mMatchesNum = 0;
            mExpMatchesNum = 0;
            imageCount = matchingAssistant.TestImageDic.Count;
            iterator = matchingAssistant.TestImageDic.Keys.GetEnumerator();
            currentIndex = 0;
        }


        /// <summary>
        /// If the optimization has stopped, then check whether it was
        /// completed successfully or whether it was aborted due to errors or
        /// to user interaction.
        /// Depending on the failure or success of the run, the GUI is notified
        /// for visual update of the results and obtained statistics.
        /// </summary>
		public override void Stop()
        {
            if (imageCount == 0)
            {
                NotifyStatisticsObserver(MatchingStatus.ErrNoTestImage);
                NotifyStatisticsObserver(MatchingStatus.RunFailed);
            }
            else if (!optSuccess && (mCurrScoreMin == 0.0))
            {
                NotifyStatisticsObserver(MatchingStatus.UpdateRecogErr);
                NotifyStatisticsObserver(MatchingStatus.RunFailed);
            }
            else if (!optSuccess)
            {
                NotifyStatisticsObserver(MatchingStatus.UpdateTestErr);
                NotifyStatisticsObserver(MatchingStatus.RunFailed);
            }
            else
            {
                StatusString = "优化成功结束";
                NotifyStatisticsObserver(MatchingStatus.UpdateRecogSattisticStatus);
                matchingAssistant.SetMinScore(mOptScoreMin / 100.0);
                matchingAssistant.SetGreediness(mOptGreediness / 100.0);
                NotifyStatisticsObserver(MatchingStatus.RunSucccessful);
            }
        }
    }//end of class
}//end of namespace

