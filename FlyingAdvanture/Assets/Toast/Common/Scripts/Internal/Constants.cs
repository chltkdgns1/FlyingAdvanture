namespace Toast
{
    using Toast.Utils;

    public class Constants
    {
        public static string URL_IAP_ALPHA
        {
            get
            {
                return StringMerger.MergeStrings("apaaiipcodtatcm", "lh-p-a.lu.os.o");
            }
        }

        public static string URL_IAP_BETA
        {
            get
            {
                return StringMerger.MergeStrings("bt-p-a.lu.os.o", "eaaiipcodtatcm");
            }
        }

        public static string URL_IAP_REAL
        {
            get
            {
                return StringMerger.MergeStrings("aiipcodtatcm", "p-a.lu.os.o");
            }
        }

        public static readonly string SdkPluginObjectName = "UnityToastSDKPlugin";
        public static readonly string SdkPluginReceiveMethodName = "ReceiveFromNative";

        public static readonly string ToastLoggerSettingObjectName = "ToastLoggerSettings";
        public static readonly string ToastLoggerFilterObjectName = "ToastLoggerFilter";
        public static readonly string ToastLoggerSenderObjectName = "ToastLoggerSender";
        public static readonly string ToastLoggerSendQueueObjectName = "ToastLoggerSendQueue";

        public static readonly string LogTransferObjectName = "LogTrasnfer";
        public static readonly string LogSendQueueObjectName = "LogSendQueue";
    }
}