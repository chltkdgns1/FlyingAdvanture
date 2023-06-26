namespace Toast.Core
{
    using Toast.Utils;

    public static class LogUrlConstants
    {
        public static string COLLECTOR_REAL_URL
        {
            get
            {
                return StringMerger.MergeStrings("hts/ailgcahcodtatcm", "tp:/p-onrs.lu.os.o");
            }
        }

        public static string COLLECTOR_BETA_URL
        {
            get
            {
                return StringMerger.MergeStrings("hts/bt-p-onrs.lu.os.o", "tp:/eaailgcahcodtatcm");
            }
        }

        public static string COLLECTOR_ALPHA_URL
        {
            get
            {
                return StringMerger.MergeStrings("hts/apaailgcahcodtatcm", "tp:/lh-p-onrs.lu.os.o");
            }
        }

        public static string SETTINGS_REAL_URL
        {
            get
            {
                return StringMerger.MergeStrings("hts/stiglgcahcodtatcm", "tp:/etn-onrs.lu.os.o");
            }
        }

        public static string SETTINGS_BETA_URL
        {
            get
            {
                return StringMerger.MergeStrings("hts/bt-etn-onrs.lu.os.o", "tp:/eastiglgcahcodtatcm");
            }
        }

        public static string SETTINGS_ALPHA_URL
        {
            get
            {
                return StringMerger.MergeStrings("hts/apastiglgcahcodtatcm", "tp:/lh-etn-onrs.lu.os.o");
            }
        }
    }
}