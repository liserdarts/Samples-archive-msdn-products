using System;

namespace Microsoft.SAPSK.ContosoTours.PPT
{
    public class StatisticList
    {
        private int _adultAgeStat = 0;

        private int _childAgeStat = 0;

        private int _infantAgeStat = 0;

        private int _firstClassStat = 0;

        private int _businessClassStat = 0;

        private int _economyClassStat = 0;

        internal StatisticList()
        { }

        public int AdultAgeStat
        {
            get { return _adultAgeStat; }
            set { _adultAgeStat = value; }
        }
        
        public int ChildAgeStat
        {
            get { return _childAgeStat; }
            set { _childAgeStat = value; }
        }
        
        public int InfantAgeStat
        {
            get { return _infantAgeStat; }
            set { _infantAgeStat = value; }
        }
        
        public int FirstClassStat
        {
            get { return _firstClassStat; }
            set { _firstClassStat = value; }
        }
        
        public int BusinessClassStat
        {
            get { return _businessClassStat; }
            set { _businessClassStat = value; }
        }
        
        public int EconomyClassStat
        {
            get { return _economyClassStat; }
            set { _economyClassStat = value; }
        }
    }
}
