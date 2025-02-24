namespace Kozubenko.Utils
{
    public static class DateTime
    {
        /// <summary>
        /// Unix time in seconds since 1970.
        /// </summary>
        public static uint UnixTimestamp
        {
            get { return ToUnixTimestamp(System.DateTime.UtcNow); }
        }

        /// <summary>
        /// Converts DateTime to seconds since 1970.
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        public static uint ToUnixTimestamp(System.DateTime? now = null)
        {
            if (now == null)
                now = System.DateTime.UtcNow;

            TimeSpan t = now.Value - new System.DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (uint)t.TotalSeconds;
        }


        public static System.DateTime ToUtcDateTime(uint unixTimestamp)
        {
            System.DateTime dt = new System.DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return dt.AddSeconds(unixTimestamp);
        }

        public static System.DateTime ToLocalDateTime(uint unixTimestamp)
        {
            System.DateTime epoch = new System.DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            System.DateTime utcTime = epoch.AddSeconds(unixTimestamp);

            return utcTime.ToLocalTime();
        }
    }
}
