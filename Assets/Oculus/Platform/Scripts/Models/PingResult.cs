namespace Oculus.Platform.Models
{
    public class PingResult
    {
        private ulong? pingTimeUsec;

        public PingResult(ulong id, ulong? pingTimeUsec)
        {
            ID = id;
            this.pingTimeUsec = pingTimeUsec;
        }

        public ulong ID { get; private set; }

        public ulong PingTimeUsec => pingTimeUsec.HasValue ? pingTimeUsec.Value : 0;

        public bool IsTimeout => !pingTimeUsec.HasValue;
    }
}