namespace WebAPI.POCOClass
{

    /// <summary>
    ///     Lưu trữ thông tin nhân viên
    ///   <br />
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 8/9/2022 created
    /// </Modified>
    public class Employee
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ExtraInfo { get; set; }

    }
}
