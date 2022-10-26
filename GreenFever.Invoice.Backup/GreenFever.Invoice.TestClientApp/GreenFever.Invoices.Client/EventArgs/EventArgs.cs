namespace GreenFever.Invoices.Client.EventArgs
{
    public class EventArgs<T> : System.EventArgs
    {
        public EventArgs(T val)
        {
            this.Value = val;
        }

        public T Value { get; private set; }
    }
}
