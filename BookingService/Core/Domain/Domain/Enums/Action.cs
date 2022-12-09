namespace Domain.Enums
{
    public enum Action
    {
        Pay = 0,
        Finish = 1, //after paid and used
        Cancel = 2, //can never be paid if canceled
        Refound = 3, //paid than refound
        Reopen = 4, //only canceled
    }
}
