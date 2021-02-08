namespace HttpInterceptor.Classes
{
    internal class ResponseDatum
    {
        public object Datum { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }
}