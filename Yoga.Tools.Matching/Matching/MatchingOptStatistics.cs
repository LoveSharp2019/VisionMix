using System;

namespace Yoga.Tools.Matching
{
    /// <summary>
    /// To determine the performance of a shape-based model, given
    /// a parameter setup for model creation and detection, 
    /// this class applies a model detection for the whole set
    /// of test images loaded and computes an all-over statistics.
    /// </summary>
    public class MatchingOptStatistics : MatchingOpt
    {
        // recognize - group 
        private int mMatchesNumProb;
        private bool mModelFound;
        private int mSpecMatchesNum;
        private int mMaxMatchesNum;
        private int mFoundMatchesNum;
        private int mImagesWithOneMatchNum;
        private int mImagesWithSpecMatchesNum;
        private int mImagesWithMaxMatchesNum;

        // statistic - group 
        private double mScoreMin;
        private double mScoreMax;
        private double mTimeMin;
        private double mTimeMax;
        private double mRowMin;
        private double mRowMax;
        private double mColMin;
        private double mColMax;
        private double mAngleMin;
        private double mAngleMax;
        private double mScaleRowMin;
        private double mScaleRowMax;
        private double mScaleColMin;
        private double mScaleColMax;


        /// <summary>Constructor</summary>
        /// <param name="mAss">MatchingAssistant that created this instance</param>
        /// <param name="mPars">Current set of matching parameters</param>
        public MatchingOptStatistics(MatchingTool mAss)
        {
            matchingAssistant = mAss;
            NotifyStatisticsObserver = new StatisticsDelegate(dummy);

            Reset();
            imageCount = matchingAssistant.TestImageDic.Count;
        }

        /// <summary>
        /// With each execution step the shape-based model is searched in
        /// the current test image. The detection result is then compared 
        /// with the previous results and the overall statistics is adjusted.
        /// </summary>
		public override bool ExecuteStep()
        {
            string fileName, imgNumStr;
            int val, i;
            string matchFormatStr;
            int actualMatches;
            int expectedMatches;
            int maxNumMatches;
            double score, time, row, col, angle, angleB, scaleR, scaleC;

            if (!iterator.MoveNext())
                return false;

            fileName = iterator.Current;
            matchingAssistant.SetTestImage(fileName);

            if (!(optSuccess = matchingAssistant.ApplyFindModel()))
                return false;

            matchingResults = matchingAssistant.MatchingResults;
            actualMatches = matchingResults.Count;

            // determine recognition rate ------------------ 
            expectedMatches = matchingAssistant.ParameterSet.RecogManualSel;
            maxNumMatches = matchingAssistant.ParameterSet.NumMatches;

            mSpecMatchesNum += expectedMatches;
            mMaxMatchesNum += maxNumMatches;
            mFoundMatchesNum += actualMatches;

            if (actualMatches > 0)
                mImagesWithOneMatchNum++;

            if (actualMatches >= expectedMatches)
                mImagesWithSpecMatchesNum++;

            if (actualMatches == maxNumMatches)
                mImagesWithMaxMatchesNum++;

            currentIndex++;

            this.InspectTabRecogRateData[2] = "-";
            this.InspectTabRecogRateData[3] = "-";
            this.InspectTabRecogRateData[4] = "-";

            imgNumStr = " / " + currentIndex + " 图像)";

            val = mImagesWithOneMatchNum;
            this.InspectTabRecogRateData[0] = Math.Round(100.0 * ((double)val / currentIndex), 2)
                                                         + " %  (" + val + imgNumStr;
            val = mImagesWithSpecMatchesNum;
            this.InspectTabRecogRateData[1] = Math.Round(100.0 * ((double)val / currentIndex), 2)
                                                         + " %  (" + val + imgNumStr;
            if (mMaxMatchesNum > 0)
            {
                matchFormatStr = " / " + mMaxMatchesNum + " 模板)";

                val = mImagesWithMaxMatchesNum;
                this.InspectTabRecogRateData[2] = Math.Round(100.0 * ((double)val / currentIndex), 2)
                                                             + " %  (" + val + imgNumStr;
                val = mFoundMatchesNum;
                this.InspectTabRecogRateData[3] = Math.Round(100.0 * ((double)val / mMaxMatchesNum), 2)
                                                             + " %  (" + val + matchFormatStr;
            }

            if (mSpecMatchesNum > 0)
            {
                matchFormatStr = " / " + mSpecMatchesNum + " 模板)";
                val = mFoundMatchesNum;
                this.InspectTabRecogRateData[4] = Math.Round(100.0 * ((double)val / mSpecMatchesNum), 2)
                                                             + " %  (" + val + matchFormatStr;
            }
            NotifyStatisticsObserver(MatchingStatus.UpdateInspRecograte);

            // determine statistics data ------------ 
            if (actualMatches > 0)
            {
                i = 0;
                if (!mModelFound)
                {
                    mScoreMin = mScoreMax = matchingResults.Score[0].D;
                    mTimeMin = mTimeMax = matchingResults.TimeFound;
                    mRowMin = mRowMax = matchingResults.Row[0].D;
                    mColMin = mColMax = matchingResults.Col[0].D;
                    mAngleMin = mAngleMax = matchingResults.Angle[0].D;
                    mScaleRowMin = mScaleRowMax = matchingResults.ScaleRow[0].D;
                    mScaleColMin = mScaleColMax = matchingResults.ScaleCol[0].D;
                    mModelFound = true;
                    i++;
                }

                for (; i < actualMatches; i++)
                {
                    score = matchingResults.Score[i].D;
                    if (score < mScoreMin)
                        mScoreMin = score;
                    else if (score > mScoreMax)
                        mScoreMax = score;

                    row = matchingResults.Row[i].D;
                    if (row < mRowMin)
                        mRowMin = row;
                    else if (row > mRowMax)
                        mRowMax = row;

                    col = matchingResults.Col[i].D;
                    if (col < mColMin)
                        mColMin = col;
                    else if (col > mColMax)
                        mColMax = col;

                    angle = matchingResults.Angle[i].D;
                    if (angle < mAngleMin)
                        mAngleMin = angle;
                    else if (angle > mAngleMax)
                        mAngleMax = angle;

                    scaleR = matchingResults.ScaleRow[i].D;
                    if (scaleR < mScaleRowMin)
                        mScaleRowMin = scaleR;
                    else if (scaleR > mScaleRowMax)
                        mScaleRowMax = scaleR;

                    scaleC = matchingResults.ScaleCol[i].D;
                    if (scaleC < mScaleColMin)
                        mScaleColMin = scaleC;
                    else if (scaleC > mScaleColMax)
                        mScaleColMax = scaleC;
                }//end of for

                time = matchingResults.TimeFound;
                if (time < mTimeMin)
                    mTimeMin = time;
                else if (time > mTimeMax)
                    mTimeMax = time;
            }//end of if


            if (mModelFound)
            {
                this.InspectTabStatisticsData[0] = "" + Math.Round(mScoreMin, 2);
                this.InspectTabStatisticsData[1] = "" + Math.Round(mScoreMax, 2);
                this.InspectTabStatisticsData[2] = "" + Math.Round((mScoreMax - mScoreMin), 2);

                this.InspectTabStatisticsData[3] = "" + Math.Round(mTimeMin, 2);
                this.InspectTabStatisticsData[4] = "" + Math.Round(mTimeMax, 2);
                this.InspectTabStatisticsData[5] = "" + Math.Round((mTimeMax - mTimeMin), 2);

                this.InspectTabStatisticsData[6] = "" + Math.Round(mRowMin, 2);
                this.InspectTabStatisticsData[7] = "" + Math.Round(mRowMax, 2);
                this.InspectTabStatisticsData[8] = "" + Math.Round((mRowMax - mRowMin), 2);

                this.InspectTabStatisticsData[9] = "" + Math.Round(mColMin, 2);
                this.InspectTabStatisticsData[10] = "" + Math.Round(mColMax, 2);
                this.InspectTabStatisticsData[11] = "" + Math.Round((mColMax - mColMin), 2);

                angle = mAngleMin * 180.0 / Math.PI;
                angleB = mAngleMax * 180.0 / Math.PI;
                this.InspectTabStatisticsData[12] = "" + Math.Round(angle, 2);
                this.InspectTabStatisticsData[13] = "" + Math.Round(angleB, 2);
                this.InspectTabStatisticsData[14] = "" + Math.Round((angleB - angle), 2);

                this.InspectTabStatisticsData[15] = "" + Math.Round(mScaleRowMin, 2);
                this.InspectTabStatisticsData[16] = "" + Math.Round(mScaleRowMax, 2);
                this.InspectTabStatisticsData[17] = "" + Math.Round((mScaleRowMax - mScaleRowMin), 2);

                this.InspectTabStatisticsData[18] = "" + Math.Round(mScaleColMin, 2);
                this.InspectTabStatisticsData[19] = "" + Math.Round(mScaleColMax, 2);
                this.InspectTabStatisticsData[20] = "" + Math.Round((mScaleColMax - mScaleColMin), 2);

                NotifyStatisticsObserver(MatchingStatus.UpdateInspStatistics);
            }
            return (currentIndex < imageCount);
        }


        /// <summary>
        /// Resets all parameters for evaluating the performance to their initial values.
        /// </summary>
		public override void Reset()
        {
            mMatchesNumProb = matchingAssistant.ParameterSet.NumMatches;
            mModelFound = false;
            mSpecMatchesNum = 0;
            mMaxMatchesNum = 0;
            mFoundMatchesNum = 0;
            mImagesWithOneMatchNum = 0;
            mImagesWithSpecMatchesNum = 0;
            mImagesWithMaxMatchesNum = 0;
            optSuccess = false;

            for (int i = 0; i < 21; i++)
                this.InspectTabStatisticsData[i] = "-";

            InspectTabRecogRateData[0] = "100.00 % (1 / 1  图像)";
            InspectTabRecogRateData[1] = "100.00 % (1 / 1  图像)";
            InspectTabRecogRateData[2] = "100.00 % (1 / 1  图像)";
            InspectTabRecogRateData[3] = "100.00 % (1 / 1 模板)";
            InspectTabRecogRateData[4] = "100.00 % (1 / 1 模板)";

            imageCount = matchingAssistant.TestImageDic.Count;
            iterator = matchingAssistant.TestImageDic.Keys.GetEnumerator();
            currentIndex = 0;
        }

        /// <summary>
        /// If the optimization has stopped, then check whether it was
        /// completed successfully or whether it was aborted 
        /// due to errors or to user interaction.
        /// Depending on the failure or success of the run, the GUI is notified
        /// for visual update of the results and obtained statistics.
        /// </summary>
		public override void Stop()
        {
            if (imageCount == 0)
            {
                NotifyStatisticsObserver(MatchingStatus.ErrNoTestImage);
            }
            else if (!optSuccess)
            {
                NotifyStatisticsObserver(MatchingStatus.UpdateTestErr);
            }
        }
    }//end of class
}//end of namespace
